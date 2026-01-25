using System;

namespace GestorDeColmenasFrontend.Dtos.Registros
{
    public class RegistroMedicionColmenaGetDto : RegistroGetDto
    {
        public int MedicionColmenaId { get; set; }
        //nos quedamos solo con las propiedades especificas derivadas del dto
        //no declaramos FechaMedicion tempExterna o Peso ya que ya estan
        //en RegistroGetDto y se deben usar esas

        //public float TempExterna { get; set; }
        //public float Peso { get; set; }
        //public DateTime FechaMedicion { get; set; }
        public int ColmenaId { get; set; }
    }
}