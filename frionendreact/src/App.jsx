
import React, { useState, useEffect } from 'react';

function App() {
    
    const [aguas, setAguas] = useState([]);
    const [usuarios, setUsuarios] = useState([]);
    const [reportes, setReportes] = useState([]);  
    const [nuevaAgua, setNuevaAgua] = useState({ zona: '', valor: 0, idUsuario: '', unidad: 'm³' });
    const [nuevoUsuario, setNuevoUsuario] = useState({ nombre: '', apellido: '', rol: '' });
    const [mensaje, setMensaje] = useState('');

    const [editandoAguaId, setEditandoAguaId] = useState(null);
    const [editandoUsuarioId, setEditandoUsuarioId] = useState(null);

    const API_BASE = 'https://localhost:44325';


    useEffect(() => {
        fetch(`${API_BASE}/api/Agua`)
            .then(response => response.json())
            .then(data => setAguas(data || []))
            .catch(() => setMensaje('Error aguas'));
    }, []);

    useEffect(() => {
        fetch(`${API_BASE}/api/Usuario`)
            .then(response => response.json())
            .then(data => setUsuarios(data || []))
            .catch(() => setMensaje('Error usuarios'));
    }, []);

    
    useEffect(() => {
        fetch(`${API_BASE}/api/Reporte`)
            .then(response => response.json())
            .then(data => setReportes(data || []))
            .catch(() => setMensaje('Error reportes'));
    }, []);

    const editarAgua = (agua) => {
        setEditandoAguaId(agua.id);
        setNuevaAgua({ ...agua });
    };

    const cancelarAgua = () => {
        setEditandoAguaId(null);
        setNuevaAgua({ zona: '', valor: 0, idUsuario: '', unidad: 'm³' });
    };

    const guardarAgua = async () => {
        try {
            if (editandoAguaId) {
                await fetch(`${API_BASE}/api/Agua/${editandoAguaId}`, {
                    method: 'PUT',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(nuevaAgua)
                });
                setMensaje('Agua actualizada');
            } else {
                await fetch(`${API_BASE}/api/Agua`, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(nuevaAgua)
                });
                setMensaje('Agua creada');
            }

           
            const [aguasData, usuariosData, reportesData] = await Promise.all([
                fetch(`${API_BASE}/api/Agua`).then(r => r.json()),
                fetch(`${API_BASE}/api/Usuario`).then(r => r.json()),
                fetch(`${API_BASE}/api/Reporte`).then(r => r.json())
            ]);

            setAguas(aguasData);
            setUsuarios(usuariosData);
            setReportes(reportesData);

            setNuevaAgua({ zona: '', valor: 0, idUsuario: '', unidad: 'm³' });
            setEditandoAguaId(null);
        } catch {
            setMensaje('Error al guardar agua');
        }
    };

    const editarUsuario = (usuario) => {
        setEditandoUsuarioId(usuario.id);
        setNuevoUsuario({ ...usuario });
    };

    const cancelarUsuario = () => {
        setEditandoUsuarioId(null);
        setNuevoUsuario({ nombre: '', apellido: '', rol: '' });
    };

    const guardarUsuario = async () => {
        try {
            if (editandoUsuarioId) {
                await fetch(`${API_BASE}/api/Usuario/${editandoUsuarioId}`, {
                    method: 'PUT',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(nuevoUsuario)
                });
                setMensaje('Usuario actualizado');
            } else {
                await fetch(`${API_BASE}/api/Usuario`, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(nuevoUsuario)
                });
                setMensaje('Usuario creado');
            }

            
            const [aguasData, usuariosData, reportesData] = await Promise.all([
                fetch(`${API_BASE}/api/Agua`).then(r => r.json()),
                fetch(`${API_BASE}/api/Usuario`).then(r => r.json()),
                fetch(`${API_BASE}/api/Reporte`).then(r => r.json())
            ]);

            setAguas(aguasData);
            setUsuarios(usuariosData);
            setReportes(reportesData);

            setNuevoUsuario({ nombre: '', apellido: '', rol: '' });
            setEditandoUsuarioId(null);
        } catch {
            setMensaje('Error al guardar usuario');
        }
    };

    const eliminarAgua = async (id) => {
        try {
            await fetch(`${API_BASE}/api/Agua/${id}`, { method: 'DELETE' });

           
            const [aguasData, usuariosData, reportesData] = await Promise.all([
                fetch(`${API_BASE}/api/Agua`).then(r => r.json()),
                fetch(`${API_BASE}/api/Usuario`).then(r => r.json()),
                fetch(`${API_BASE}/api/Reporte`).then(r => r.json())
            ]);

            setAguas(aguasData);
            setUsuarios(usuariosData);
            setReportes(reportesData);
        } catch {
            setMensaje('Error eliminar');
        }
    };

    const eliminarUsuario = async (id) => {
        try {
            await fetch(`${API_BASE}/api/Usuario/${id}`, { method: 'DELETE' });

            
            const [aguasData, usuariosData, reportesData] = await Promise.all([
                fetch(`${API_BASE}/api/Agua`).then(r => r.json()),
                fetch(`${API_BASE}/api/Usuario`).then(r => r.json()),
                fetch(`${API_BASE}/api/Reporte`).then(r => r.json())
            ]);

            setAguas(aguasData);
            setUsuarios(usuariosData);
            setReportes(reportesData);
        } catch {
            setMensaje('Error eliminar');
        }
    };
    const exportarReportesPDF = async () => {
        const jsPDF = (await import('jspdf')).default;
        const autoTable = (await import('jspdf-autotable')).default;

        const doc = new jsPDF('l', 'mm'); 
        doc.text('Reporte de Reportes', 14, 20);

        
        const tableData = reportes.map(reporte => {
            const usuario = usuarios.find(u => String(u.id) === String(reporte.idUsuario));
            const agua = aguas.find(a => String(a.id) === String(reporte.id));
            const aguaFinal = agua ||
                (usuarios.find(u => String(u.id) === String(reporte.idUsuario)) &&
                    aguas.filter(a => String(a.idUsuario) === String(reporte.idUsuario))[0]);

            return [
                reporte.id,
                reporte.idUsuario || 'Sin ID',
                usuario ? `${usuario.nombre} ${usuario.apellido || ''}` : 'Sin usuario',
                aguaFinal?.zona || 'Sin datos',
                aguaFinal?.valor || 'Sin datos',
                aguaFinal?.unidad || 'm³'
            ];
        });

        autoTable(doc, {
            head: [['ID Reporte', 'ID Usuario', 'Usuario', 'Zona Agua', 'Valor Agua', 'Unidad']],
            body: tableData,
            startY: 30,
            margin: { top: 30, left: 5, right: 5 }, 
            styles: {
                fontSize: 9,
                cellPadding: 4,
                overflow: 'linebreak',
                halign: 'left' 
            },
            headStyles: {
                fillColor: [41, 128, 185],
                textColor: 255,
                fontStyle: 'bold',
                fontSize: 10
            },
            tableWidth: 285, 
            columnStyles: {
                0: { cellWidth: 48 }, 
                1: { cellWidth: 48 }, 
                2: { cellWidth: 48 }, 
                3: { cellWidth: 42 }, 
                4: { cellWidth: 42 }, 
                5: { cellWidth: 57 }  
            }
        });

        doc.save('reportes.pdf');
    };




    return (
        <div>
            <h1>CRUD</h1>

            <h2>Aguas</h2>
            <table border="1">
                <thead>
                    <tr>
                        <th>ID</th><th>Zona</th><th>Valor</th><th>Unidad</th><th>Usuario</th><th>Accion</th>
                    </tr>
                </thead>
                <tbody>
                    {aguas.map(agua => (
                        <tr key={agua.id}>
                            <td>{agua.id}</td>
                            <td>{agua.zona}</td>
                            <td>{agua.valor}</td>
                            <td>{agua.unidad || 'm³'}</td>
                            <td>{agua.idUsuario}</td>
                            <td>
                                {editandoAguaId === agua.id ? (
                                    <>
                                        <button onClick={guardarAgua}>Actualizar</button>
                                        <button onClick={cancelarAgua}>Cancelar</button>
                                    </>
                                ) : (
                                    <>
                                        <button onClick={() => editarAgua(agua)}>Editar</button>
                                        <button onClick={() => eliminarAgua(agua.id)}>Eliminar</button>
                                    </>
                                )}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            <div>
                <input placeholder="Zona" value={nuevaAgua.zona} onChange={e => setNuevaAgua({ ...nuevaAgua, zona: e.target.value })} />
                <input type="number" placeholder="Valor" value={nuevaAgua.valor} onChange={e => setNuevaAgua({ ...nuevaAgua, valor: Number(e.target.value) })} />
                <input placeholder="IdUsuario" value={nuevaAgua.idUsuario} onChange={e => setNuevaAgua({ ...nuevaAgua, idUsuario: e.target.value })} />
                <button onClick={guardarAgua}>
                    {editandoAguaId ? 'Actualizar Agua' : 'Crear Agua'}
                </button>
            </div>

            <h2>Usuarios</h2>
            <table border="1">
                <thead>
                    <tr>
                        <th>ID</th><th>Nombre</th><th>Apellido</th><th>Rol</th><th>Accion</th>
                    </tr>
                </thead>
                <tbody>
                    {usuarios.map(usuario => (
                        <tr key={usuario.id}>
                            <td>{usuario.id}</td>
                            <td>{usuario.nombre}</td>
                            <td>{usuario.apellido}</td>
                            <td>{usuario.rol}</td>
                            <td>
                                {editandoUsuarioId === usuario.id ? (
                                    <>
                                        <button onClick={guardarUsuario}>Actualizar</button>
                                        <button onClick={cancelarUsuario}>Cancelar</button>
                                    </>
                                ) : (
                                    <>
                                        <button onClick={() => editarUsuario(usuario)}>Editar</button>
                                        <button onClick={() => eliminarUsuario(usuario.id)}>Eliminar</button>
                                    </>
                                )}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            <div>
                <input placeholder="Nombre" value={nuevoUsuario.nombre} onChange={e => setNuevoUsuario({ ...nuevoUsuario, nombre: e.target.value })} />
                <input placeholder="Apellido" value={nuevoUsuario.apellido} onChange={e => setNuevoUsuario({ ...nuevoUsuario, apellido: e.target.value })} />
                <input placeholder="Rol" value={nuevoUsuario.rol} onChange={e => setNuevoUsuario({ ...nuevoUsuario, rol: e.target.value })} />
                <button onClick={guardarUsuario}>
                    {editandoUsuarioId ? 'Actualizar Usuario' : 'Crear Usuario'}
                </button>
            </div>

            
            
            <h2>Reportes</h2>
            <div style={{ marginBottom: '10px' }}>
                <button
                    onClick={exportarReportesPDF}
                    style={{ padding: '8px 16px', backgroundColor: '#28a745', color: 'white', border: 'none', borderRadius: '4px', cursor: 'pointer' }}
                >
                    📄 Exportar Reportes PDF
                </button>
            </div>
            <table border="1" >
                <thead>
                    <tr>
                        <th>ID Reporte</th>
                        <th>ID Usuario</th>
                        <th>Usuario</th>
                        <th>Zona Agua</th>
                        <th>Valor Agua</th>
                        <th>Unidad</th>
                    </tr>
                </thead>
                <tbody>
                    {reportes.map(reporte => {
                   
                        const usuario = usuarios.find(u => String(u.id) === String(reporte.idUsuario));

                       
                        const agua = aguas.find(a => String(a.id) === String(reporte.id));

                        
                        const aguaFinal = agua ||
                            (usuarios.find(u => String(u.id) === String(reporte.idUsuario)) &&
                                aguas.filter(a => String(a.idUsuario) === String(reporte.idUsuario))[0]);

                        return (
                            <tr key={reporte.id}>
                                <td>{reporte.id}</td>
                                <td>{reporte.idUsuario || 'Sin ID'}</td>
                                <td>{usuario ? `${usuario.nombre} ${usuario.apellido || ''}` : 'Sin usuario'}</td>
                                <td>{aguaFinal?.zona || 'Sin datos'}</td>
                                <td>{aguaFinal?.valor || 'Sin datos'}</td>
                                <td>{aguaFinal?.unidad || 'm³'}</td>
                            </tr>
                        );
                    })}
                </tbody>
            </table>



            <p>{mensaje}</p>
        </div>
    );
    

}

export default App;
