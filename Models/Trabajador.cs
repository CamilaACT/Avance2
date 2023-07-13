namespace Avance.Models
{
    public class Trabajador
    {
        public int COMP_Codigo { get; set; }
        public int Id_Trabajador { get; set; }
        public string Tipo_trabajador { get; set; }
        public string Apellido_Paterno { get; set; }
        public string Apellido_Materno { get; set; }
        public string Nombres { get; set; }
        public string Identificacion { get; set; }
        public string Entidad_Bancaria { get; set; }
        public string CarnetIESS { get; set; }
        public string Direccion { get; set; }
        public string Telefono_Fijo { get; set; }
        public string Telefono_Movil { get; set; }
        public string Genero { get; set; }
        public string Nro_Cuenta_Bancaria { get; set; }
        public string Codigo_Categoria_Ocupacion { get; set; }
        public string Ocupacion { get; set; }
        public string Centro_Costos { get; set; }
        public string Nivel_Salarial { get; set; }
        public string EstadoTrabajador { get; set; }
        public string Tipo_Contrato { get; set; }
        public string Tipo_Cese { get; set; }
        public string EstadoCivil { get; set; }
        public string TipodeComision { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaCese { get; set; }
        public string PeriododeVacaciones { get; set; }
        public DateTime FechaReingreso { get; set; }
        public DateTime Fecha_Ult_Actualizacion { get; set; }
        public bool EsReingreso { get; set; }
        public string BancoCTA_CTE { get; set; }
        public string Tipo_Cuenta { get; set; }
        public string RSV_Indem_Acumul { get; set; }
        public int Año_Ult_Rsva_Indemni { get; set; }
        public string Mes_Ult_Rsva_Indemni { get; set; }
        public string FormaCalculo13ro { get; set; }
        public string FormaCalculo14ro { get; set; }
        public string BoniComplementaria { get; set; }
        public string BoniEspecial { get; set; }
        public decimal Remuneracion_Minima { get; set; }
        public decimal CuotaCuentaCorriente { get; set; }
        public decimal Fondo_Reserva { get; set; }
    }

}
