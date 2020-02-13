using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep Find(VaultKeep vk)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE (keepId = @KeepId AND vaultId = @VaultId)";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, vk);
    }

    internal VaultKeep Find(int VId, int KId)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE (keepId = @KId AND vaultId = @VId)";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { VId, KId });
    }

    internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId, string userId)
    {
      string sql = @"SELECT k.* FROM vaultkeeps vk
INNER JOIN keeps k ON k.id = vk.keepId 
WHERE (vaultId = @vaultId AND vk.userId = @userId)";
      return _db.Query<Keep>(sql, new { vaultId, userId });
    }


    internal string Create(VaultKeep newData)
    {
      string sql = @"
            INSERT INTO vaultkeeps 
            (keepId, vaultId, userId) 
            VALUES 
            (@KeepId, @VaultId, @UserId);
            SELECT LAST_INSERT_ID();
            ";
      _db.Execute(sql, newData);
      return "VaultKeep";
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaultkeeps WHERE id = @id";
      _db.Execute(sql, new { id });
    }
  }
}
