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

    internal string Delete(VaultKeep vks)
    {
      VaultKeep exists = _repo.Find(vks);
      if (exists == null) { throw new Exception("Invalid Id Combination"); }
      _repo.Delete(exists.Id);
      return "Successfully Deleted";
    }
  }
}
