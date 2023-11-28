using Ninject.Selection;
using Semestralni_prace.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_prace.Models.Classes
{
    public class Zvire
    {
        public string? JmenoZvire { get; internal set; }
        public string? Pohlavi { get; internal set; }
        public DateTime DatumNarozeni { get; internal set; }
        public DateTime? DatumUmrti { get; internal set; }
        public int MajitelZvireIdPacient { get; internal set; }
        public int IdZvire { get; internal set; }
        public int RasaZviratIdRasa { get; internal set; }
    }
}
/* Funguje, ale blbě
SELECT
    za.JMENO AS ZAMESTNANEC_KRESTNI,
    za.PRIJMENI AS ZAMESTNANEC_PRIJMENI,
    za.PROFESE AS ZAMESTNANEC_PROFESE,
    v.NAZEV_VAKCINA AS VAKCINA_NAZEV,
    z.JMENO_ZVIRE AS ZVIRE_JMENO,
    z.POHLAVI AS ZVIRE_POHLAVI,
    z.DATUM_NAROZENI AS ZVIRE_DATUM_NAROZENI,
    z.DATUM_UMRTI AS ZVIRE_DATUM_UMRTI
FROM
    ZAMESTNANCI za
JOIN ASISTENT_PODA_VAKCINU apv ON za.ID_ZAMESTNANEC = apv.ASISTENT_ID_ZAMESTNANEC
JOIN VAKCINY v ON apv.VAKCINA_ID_VAKCINA = v.ID_VAKCINA
JOIN VAKCINA_PODAVANA_ZVIRETI vpz ON v.ID_VAKCINA = vpz.VAKCINA_ID_VAKCINA
JOIN ZVIRATA z ON vpz.ZVIRE_ID_ZVIRE = z.ID_ZVIRE*/