﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_Práce.Classes
{
    public class Vakcina
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdVakcina { get; internal set; }
        public string? NazevVakcina { get; internal set; }
        //TODO: tady mám asi chybu v modelu, že je prodává Asistent... takže to bych s tebou probral ještě.
        //Jestli přidáme datum podání vakcíny a potom sem přidáme AsistentId, který jí administroval
        //nebo nevím jak jinak to vyřešit

    }
}