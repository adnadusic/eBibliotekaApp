// MarketUserEntity.cs
using Market.Domain.Common;
using System;
using System.Collections.Generic;

namespace Market.Domain.Entities.Identity
{
    public enum StatusKorisnika
    {
        Aktiviran,
        Blokiran,
        NaCekanju
    }

    public sealed class MarketUserEntity : BaseEntity
    {
        // PODACI ZA LOGIRANJE
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        // DODATNI PODACI O OSOBI
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public DateTime? DatumRodjenja { get; set; }

        // STATUS
        public StatusKorisnika Status { get; set; } = StatusKorisnika.Aktiviran;
        public int BrojUpozorenja { get; set; } = 0;
        public decimal UkupnoKazni { get; set; } = 0;

        // ROLE
        public bool IsAdmin { get; set; }
        public bool IsManager { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsClient { get; set; }

        // TOKENI
        public int TokenVersion { get; set; } = 0;
        public bool IsEnabled { get; set; }

        public ICollection<RefreshTokenEntity> RefreshTokens { get; private set; } = new List<RefreshTokenEntity>();
    }
}
