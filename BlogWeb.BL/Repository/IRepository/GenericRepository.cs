﻿using BlogWeb.DL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.BL.Repository.IRepository
{
    public class GenericRepository<T> where T : class, new()
    {
        Context c = new Context();

        public List<T> TList()
        {
            return c.Set<T>().ToList();
        }
        public List<T> TList(string p)
        {
            return c.Set<T>().Include(p).ToList();
        }

        public void TAdd(T t)
        {
            c.Set<T>().Add(t);
            c.SaveChanges();
        }
        public void TDelete(T t)
        {
            c.Set<T>().Remove(t);
            c.SaveChanges();
        }
        public void TUpdate(T t)
        {
            c.Set<T>().Update(t);
            c.SaveChanges();
        }
        public T TGet(int id)
        {
            return c.Set<T>().Find(id);
        }

       
    }
}
