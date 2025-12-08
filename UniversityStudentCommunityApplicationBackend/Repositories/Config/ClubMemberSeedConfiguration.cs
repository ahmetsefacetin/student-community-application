using Entities.Enums;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Config
{

    public class ClubMemberSeedConfiguration : IEntityTypeConfiguration<ClubMembership>
    {
        public void Configure(EntityTypeBuilder<ClubMembership> builder)
        {


            // Admin kullanıcısı oluştur
            var member = new ClubMembership
            {
                Id = 99999,
                ClubId = 1, // İlk kulübün Id'si
                UserId = "5342e9a1-4ba0-424a-ad00-e4a5d482f272",
                Role = ClubRole.Member,
                JoinedAt = DateTime.UtcNow
            };


            builder.HasData(member);
        }
    }

}
