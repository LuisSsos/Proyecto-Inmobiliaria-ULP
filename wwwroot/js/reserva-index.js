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
        abrirModalTerminar(id, fechaDesde, fechaHasta, montoPorDia) {
            this.errorMsg = '';
            this.fechaMin = fechaDesde;
            this.fechaMax = fechaHasta;
            
            this.form.idReserva = id;
            this.form.fechaFinReal = new Date().toISOString().split('T')[0];
            this.form.multa = 0;

            this.montoPorDia = montoPorDia;
            this.modalBootstrap.show();
        },

        calcularMulta() {

            if (!this.form.fechaFinReal) {
                this.form.multa = 0;
                return;
            }

            const fechaDesde = new Date(this.fechaMin);
            const fechaHasta = new Date(this.fechaMax);
            const fechaFinReal = new Date(this.form.fechaFinReal);

            const diasTotales = Math.round (fechaHasta - fechaDesde);
            const diasRestantes = Math.round(fechaHasta - fechaFinReal);
            const totalRestante = diasRestantes * this.montoPorDia;
            const milisegundosDia = 1000 * 60 * 60 * 24;

            if (diasRestantes >= diasTotales * 0.5) {
                this.form.multa = (totalRestante * 0.50)/milisegundosDia;
            }
            else if (diasRestantes > 0) {
                this.form.multa = (totalRestante * 0.25)/milisegundosDia;
            }
            else {
                this.form.multa = 0;
            }
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