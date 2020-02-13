using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository vkr)
    {
      _repo = vkr;
    }
    internal IEnumerable<Keep> GetByVaultId(int id, string userId)
    {
      IEnumerable<Keep> results = _repo.GetKeepsByVaultId(id, userId);
      return results;
    }

    internal void Create(VaultKeep newData)
    {
      VaultKeep exists = _repo.Find(newData);
      if (exists == null)
      {
        _repo.Create(newData);
      }
      return;
    }

    internal string Delete(int id1, int id2, string userId)
    {
      VaultKeep exists = _repo.Find(id1, id2);
      if (exists == null) { throw new Exception("Invalid Id Combination"); }
      if (exists.UserId == userId)
      {
        _repo.Delete(exists.Id);
        return "Successfully Deleted";
      }
      else
      {
        throw new Exception("You cannot delete keeps you didn't create");
      }
    }
  }
}
