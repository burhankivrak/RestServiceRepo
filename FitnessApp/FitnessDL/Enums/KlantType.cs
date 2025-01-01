﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FitnessDL.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum KlantType
    {
        Bronze,
        Silver,
        Gold
    }

}
