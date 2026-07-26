let mapa = null;
let marcador = null;
let dotNetRef = null;

window.inicializarMapa = (elementId, latInicial, lngInicial, refDotNet) => {
    dotNetRef = refDotNet;

    // Coordenadas por defecto: Santa Cruz de la Sierra, Bolivia
    const lat = latInicial || -17.7833;
    const lng = lngInicial || -63.1821;

    mapa = L.map(elementId).setView([lat, lng], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '© OpenStreetMap contributors'
    }).addTo(mapa);

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

        dotNetRef.invokeMethodAsync('ActualizarCoordenadas', lat, lng);
    });
};

window.destruirMapa = () => {
    if (mapa) {
        mapa.remove();
        mapa = null;
        marcador = null;
    }
};