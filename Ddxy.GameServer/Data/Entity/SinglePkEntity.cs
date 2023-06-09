using System;
using FreeSql.DataAnnotations;

namespace Ddxy.GameServer.Data.Entity
{
    [Table(Name = "singlePk")]
    public class SinglePkEntity : IEquatable<SinglePkEntity>
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public uint Id { get; set; }

        [Column(Name = "sid")] public uint ServerId { get; set; }

        public uint Season { get; set; }

        public string Pkzs { get; set; }

        public uint LastTime { get; set; }

        public void CopyFrom(SinglePkEntity other)
        {
            Id = other.Id;
            ServerId = other.ServerId;
            Season = other.Season;
            Pkzs = other.Pkzs;
            LastTime = other.LastTime;
        }

        public bool Equals(SinglePkEntity other)
        {
            if (other == null) return false;
            return Id == other.Id && ServerId == other.ServerId &&
                   Season == other.Season && Pkzs.Equals(other.Pkzs) && LastTime == other.LastTime;
        }
    }
}