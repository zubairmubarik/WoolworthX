﻿using MongoDB.Driver;
using MyDemoProject001.Application.Common.Interfaces;
using MyDemoProject001.Domain.Entities;

namespace MyDemoProject001.Application.Common.Logic
{
    public class SortByDescendingName : ISortingStrategy
    {
        public SortDefinition<ProductDocument> Sort()
        {
            return Builders<ProductDocument>.Sort.Descending(x => x.Name);
        }
    }
}
