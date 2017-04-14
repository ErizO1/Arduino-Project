using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Arduino_WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WCF_Windows_Service" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WCF_Windows_Service.svc o WCF_Windows_Service.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WCF_Windows_Service : IWCF_Windows_Service
    {
        public void setCurrentArduinoValue(int Value)
        {
            GlobalData.CurrentArduinoValue = Value;
        }

        public getCurrentArduinoValueResult getCurrentArduinoValue()
        {
            getCurrentArduinoValueResult r = new getCurrentArduinoValueResult()
            {
                Value = GlobalData.CurrentArduinoValue
            };
            return r;
        }
    }
}
