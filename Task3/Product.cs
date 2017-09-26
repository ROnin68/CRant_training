﻿using System;

namespace Task3
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class UseForEqualityCheck : Attribute
    {
    }

    public class Product
    {
        public int ID   { get; set; }

        [UseForEqualityCheck]
        public string Code { get; set; }

        [UseForEqualityCheck] 
        public string Name { get; set; }

        public double Price { get; set; }

        // Task3-specific: a new property to check migrations
        [UseForEqualityCheck]
        public int GroupID { get; set; }
    }
}