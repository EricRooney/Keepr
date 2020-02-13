using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Keep> Get()
    {
      return _repo.Get();
    }

    internal Keep GetById(int id, string UserId)
    {
      Keep exists = _repo.GetById(id);
      if (exists == null) { throw new Exception("Invalid Id"); }
      if (exists.UserId != UserId) { if (exists.IsPrivate == true) { throw new Exception("Invalid Id"); } else { return exists; } }
      else
      {
        return exists;
      }
    }
    internal Keep GetById(int id)
    {
      Keep exists = _repo.GetById(id);
      if (exists == null) { throw new Exception("Invalid Id"); }
      if (exists.IsPrivate == true) { throw new Exception("Invalid Id"); }
      return exists;
    }

    public Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }

    internal Keep Edit(Keep update)
    {
      var data = _repo.GetById(update.Id);
      if (data == null) { throw new Exception("Invalid Update Id"); }
      if (data.UserId == update.UserId)
      {
        _repo.Edit(update);
        return update;
      }
      else
      {
        throw new Exception("You cannot edit keeps you didn't create");
      }
    }

    internal string Delete(int id, string userId)
    {
      var exists = _repo.GetById(id);
      if (exists == null) { throw new Exception("Invalid Id"); }
      if (exists.UserId == userId)
      {
        _repo.Delete(id);
        return "Successfully Deleted";
      }
      else
      {
        throw new Exception("You cannot delete keeps you didn't create");
      }
    }
  }
}