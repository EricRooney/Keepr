using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Vault> Get(string userId)
    {
      string sql = "SELECT * FROM vaults WHERE userId = userId";
      return _db.Query<Vault>(sql);
    }

    internal Vault GetById(int Id)
    {
      string sql = "SELECT * FROM vaults WHERE id = @Id";
      return _db.QueryFirstOrDefault<Vault>(sql, new { Id });
    }

    internal Vault Create(Vault VaultData)
    {
      string sql = @"
            INSERT INTO vaults 
            (name, userId, description) 
            VALUES 
            (@Name, @UserId, @Description);
            SELECT LAST_INSERT_ID();
            ";
      int id = _db.ExecuteScalar<int>(sql, VaultData);
      VaultData.Id = id;
      return VaultData;
    }

    internal void Edit(Vault update)
    {

      string sql = @"
            UPDATE vaults
            SET 
            name = @Name, description = @Description
            WHERE id = @Id; 
            ";
      _db.Execute(sql, update);
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id";
      _db.Execute(sql, new { id });
    }
  }
}