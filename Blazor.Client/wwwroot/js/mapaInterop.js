const mapas = {};

window.inicializarMapa = (elementId, latInicial, lngInicial, refDotNet) => {
    const lat = latInicial || -17.7833;
    const lng = lngInicial || -63.1821;

    const mapa = L.map(elementId).setView([lat, lng], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(mapa);

    let marcador = null;
    if (latInicial && lngInicial) {
        marcador = L.marker([lat, lng]).addTo(mapa);
    }

    mapa.on('click', function (e) {
        const { lat, lng } = e.latlng;
        if (marcador) {
            marcador.setLatLng([lat, lng]);
        } else {
            marcador = L.marker([lat, lng]).addTo(mapa);
        }
        refDotNet.invokeMethodAsync('ActualizarCoordenadas', lat, lng);
    });

    mapas[elementId] = {
        mapa: mapa,
        marcador: () => marcador,
        setMarcador: (m) => marcador = m
    };
};

window.moverMarcador = (elementId, lat, lng) => {
    const entrada = mapas[elementId];
    if (!entrada) return;

    if (entrada.marcador()) {
        entrada.marcador().setLatLng([lat, lng]);
    } else {
        const nuevoMarcador = L.marker([lat, lng]).addTo(entrada.mapa);
        entrada.setMarcador(nuevoMarcador);
    }
    entrada.mapa.setView([lat, lng], entrada.mapa.getZoom());
};

window.deshabilitarClicMapa = (elementId) => {
    const entrada = mapas[elementId];
    if (entrada) {
        entrada.mapa.off('click');
    }
};

window.destruirMapa = (elementId) => {
    if (mapas[elementId]) {
        mapas[elementId].mapa.remove();
        delete mapas[elementId];
    }
};
