using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<SliderHome> SlidersHome { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<SomeHotel> SomeHotels { get; set; }
        public DbSet<SomeBlog> SomeBlogs { get; set; }
        public DbSet<SliderAbout> SlidersAbout { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Hotels> Hotels{ get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomImages> RoomImages { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
