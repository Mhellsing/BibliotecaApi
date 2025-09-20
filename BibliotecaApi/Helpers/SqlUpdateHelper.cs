using Dapper;

namespace BibliotecaApi.Helpers
{
	public static class SqlUpdateHelper
	{
		public static (string, DynamicParameters) MontarUpdate<T>(
			string nomeTabela,
			string campoChave,
			object valorChave,
			Dictionary<string, object?> campos)
		{
			string? sql = $"UPDATE {nomeTabela} SET ";
			DynamicParameters parametros = new DynamicParameters();
			int camposAdicionados = 0;

			foreach (KeyValuePair<string, object> campo in campos)
			{
				if (campo.Value != null && !(campo.Value is string s && string.IsNullOrEmpty(s)))
				{
					sql += $"{campo.Key} = @{campo.Key}, ";
					parametros.Add(campo.Key, campo.Value);
					camposAdicionados++;
				}
			}

			sql = sql.TrimEnd(',', ' ');

			sql += $" WHERE {campoChave} = @{campoChave}";
			parametros.Add(campoChave, valorChave);

			if (camposAdicionados == 0)
				throw new InvalidOperationException("Nenhum valor foi fornecido para atualização.");

			return (sql, parametros);
		}
	}

}
