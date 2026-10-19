using System;
using System.Collections.Generic;
using System.Text;

namespace Pogoda
{
    class WindDirectionHelper
    {
        public static string GetWindDirectionIcon(double windDirection)
        {
            if(windDirection >= 337.5 || windDirection < 22.5)
            {
                return "\ue353"; // Północ
            }
            else if(windDirection >= 22.5 && windDirection < 67.5)
            {
                return "\ue352"; // Północny-wschód
            }
            else if(windDirection >= 67.5 && windDirection < 112.5)
            {
                return "\ue349"; // Wschód
            }
            else if(windDirection >= 112.5 && windDirection < 157.5)
            {
                return "\ue380"; // Południowy-wschód
            }
            else if(windDirection >= 157.5 && windDirection < 202.5)
            {
                return "\ue340"; // Południe
            }
            else if(windDirection >= 202.5 && windDirection < 247.5)
            {
                return "\ue33f"; // Południowy-zachód
            }
            else if(windDirection >= 247.5 && windDirection < 292.5)
            {
                return "\ue344"; // Zachód
            }
            else
            {
                return "\ue37f"; // Północny-zachód
            }
        }
    }
}
