namespace Pogoda
{
    public static class WeatherCodeHelper
    {
        public static string GetDescription(int weatherCode)
        {
            return weatherCode switch
            {
                0 => "Bezchmurnie",
                1 => "Przeważnie pogodnie",
                2 => "Częściowe zachmurzenie",
                3 => "Pochmurno",

                45 => "Mgła",
                48 => "Mgła osadzająca szadź",

                51 => "Lekka mżawka",
                53 => "Umiarkowana mżawka",
                55 => "Gęsta mżawka",
                56 => "Lekka marznąca mżawka",
                57 => "Gęsta marznąca mżawka",

                61 => "Słaby deszcz",
                63 => "Umiarkowany deszcz",
                65 => "Silny deszcz",
                66 => "Słaby deszcz marznący",
                67 => "Silny deszcz marznący",

                71 => "Słabe opady śniegu",
                73 => "Umiarkowane opady śniegu",
                75 => "Silne opady śniegu",
                77 => "Ziarna śniegu",

                80 => "Przelotne słabe opady deszczu",
                81 => "Przelotne umiarkowane opady deszczu",
                82 => "Przelotne intensywne opady deszczu",

                85 => "Przelotne słabe opady śniegu",
                86 => "Przelotne intensywne opady śniegu",

                95 => "Burza",
                96 => "Burza z lekkim gradem",
                99 => "Burza z silnym gradem",

                _ => "Brak opisu pogody"
            };
        }
        public static string GetIcon(int weatherCode)
        {
            return weatherCode switch
            {
                0 => "\udb81\udd9a", // Bezchmurnie
                1 => "\udb81\udd99", // Przeważnie pogodnie
                2 => "\udb81\udd95", // Częściowe zachmurzenie
                3 => "\udb81\udd90", // Pochmurno
                45 => "\udb81\udd91", // Mgła
                48 => "\udb81\udd91", // Mgła osadzająca szadź
                51 => "\udb83\udf33", // Lekka mżawka
                53 => "\udb81\udd97", // Umiarkowana mżawka
                55 => "\udb81\udd97", // Gęsta mżawka
                56 => "\udb81\ude7f", // Lekka marznąca mżawka
                57 => "\udb81\ude7f", // Gęsta marznąca mżawka
                61 => "\udb83\udf33", // Słaby deszcz
                63 => "\udb81\udd97", // Umiarkowany deszcz
                65 => "\udb81\udd96", // Silny deszcz
                66 => "\udb81\ude7f", // Słaby deszcz marznący
                67 => "\udb81\ude7f", // Silny deszcz marznący
                71 => "\udb83\udf34", // Słabe opady śniegu
                73 => "\udb81\udd98", // Umiarkowane opady śniegu
                75 => "\udb83\udf36", // Silne opady śniegu
                77 => "\udb81\udd98", // Ziarna śniegu
                80 => "\udb83\udf33", // Przelotne słabe opady deszczu
                81 => "\udb81\udd97", // Przelotne umiarkowane opady deszczu
                82 => "\udb81\udd96", // Przelotne intensywne opady deszczu
                85 => "\udb83\udf34", // Przelotne słabe opady śniegu
                86 => "\udb83\udf36", // Przelotne intensywne opady śniegu
                95 => "\udb81\udd93", // Burza
                96 => "\udb81\udd92", // Burza z lekkim gradem
                99 => "\udb81\udd92",  // Burza z silnym gradem
                _ => "\udb80\uded7"  // Brak opisu pogody
            };
        }
    }
}