const { createApp } = Vue;

createApp({
    data() {
        return {
            modalBootstrap: null,
            guardando: false,
            errorMsg: '',
            fechaMin: '',
            fechaMax: '',
            form: {
                idReserva: 0,
                fechaFinReal: '',
                multa: 0
            }
        }
    },
    mounted() {
        const elModal = document.getElementById('modalTerminar');
        if (elModal) {
            this.modalBootstrap = new bootstrap.Modal(elModal);
        }
    },
    methods: {
        abrirModalTerminar(id, fechaDesde, fechaHasta) {
            this.errorMsg = '';
            this.fechaMin = fechaDesde;
            this.fechaMax = fechaHasta;
            
            this.form.idReserva = id;
            this.form.fechaFinReal = new Date().toISOString().split('T')[0];
            this.form.multa = 0;

            this.modalBootstrap.show();
        },
        async guardarTerminacion() {
            if (!this.form.fechaFinReal) {
                this.errorMsg = 'Por favor seleccioá una fecha válida.';
                return;
            }

            this.guardando = true;
            this.errorMsg = '';

            try {
                const response = await axios.post('/Reserva/TerminarAnticipadoJson', this.form);

                if (response.data.success) {
                    this.modalBootstrap.hide();
                    window.location.reload();
                } else {
                    this.errorMsg = response.data.message;
                }
            } catch (err) {
                this.errorMsg = 'Ocurrió un error al procesar la solicitud.';
            } finally {
                this.guardando = false;
            }
        }
    }
}).mount('#app');