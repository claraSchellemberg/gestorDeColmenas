using GestorDeColmenasFrontend.Extensions;
using GestorDeColmenasFrontend.Modelos;
using System;
using System.Collections.Generic;

namespace GestorDeColmenasFrontend.Dtos.Registros
{
    public class RegistroGetDto
    {
        // Metadatos del registro (ya existentes)
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool EstaPendiente { get; set; }
        public bool ValorEstaEnRangoBorde { get; set; }
        public List<string>? MensajesAlerta { get; set; } = new();
        public string? TipoRegistro { get; set; }

        // Propiedades que la vista esperaba — pueden quedar con valores por defecto
        public DateTime FechaMedicion { get; set; }
        public float TempInterna1 { get; set; }
        public float TempInterna2 { get; set; }
        public float TempInterna3 { get; set; }
        public float TempExterna { get; set; }
        public float Peso { get; set; }

        // Estado / texto para mostrar (Estado puede venir del backend, EstadoTexto usa extensión)
        public CondicionColmena? Estado { get; set; }
        public string? MensajeEstado { get; set; }
        public string EstadoTexto => Estado.HasValue ? Estado.ToDisplayText() : string.Empty;

        public RegistroGetDto() { }
    }
}
