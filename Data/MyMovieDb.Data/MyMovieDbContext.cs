using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyMovieDb.Data
{
    public class MyMovieDbContext : IdentityDbContext
    {
        public MyMovieDbContext(DbContextOptions<MyMovieDbContext> options)
            : base(options)
        {
        }
    }
}
