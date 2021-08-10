﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyProxyAPI.Helpers;

namespace SpotifyProxyAPI.Models
{
    public class UserSettings
    {
        public string BasePath { get; set; }
        public string EnableMiniProfiler { get; set; }
        public SpotifySettings SpotifySettings { get; set; }
        public AuditLog AuditLog { get; set; }
    }
}
