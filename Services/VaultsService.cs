using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Vault> Get(string userId)
    {
      return _repo.Get(userId);
    }

    internal Vault GetById(int id, string userId)
    {
      var exists = _repo.GetById(id);
      if (exists == null) { throw new Exception("Invalid Id"); }
      if (exists.UserId == userId)
      {
        return exists;
      }
      else
      {
        throw new Exception("You cannot get vaults you didn't create");
      }
    }

    public Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }

    internal Vault Edit(Vault update)
    {
      var data = _repo.GetById(update.Id);
      if (data == null) { throw new Exception("Invalid Update Id"); }

      // update.AuthorId = data.AuthorId

      _repo.Edit(update);
      return update;
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
        throw new Exception("You cannot delete vaults you didn't create");
      }
    }
  }
}