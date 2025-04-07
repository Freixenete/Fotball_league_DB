using MySql.Data.MySqlClient;

namespace Fotball_league;

class Program
{
    static void Main(string[] args)
    {
        MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;" +
                                                   "user id=root;password=ball;database=futbol");

        conn.Open();
        
        
        Console.WriteLine("Que temporada quieres ver?");
        int temporada = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Ordenar por goles o puntos?");
        string ordenarPor = Console.ReadLine()?.ToLower();

        if (ordenarPor != "gols")
        {
            ordenarPor = "punts";
        }
        else
        {
            ordenarPor = "punts";
        }

        string query = $@"SELECT e.nom AS Equipo, r.{ordenarPor} AS {ordenarPor}
                        FROM resultats r
                        JOIN equips e ON r.equip = e.id
                        WHERE r.temporada = @temporada
                        ORDER BY r.{ordenarPor} DESC";

        using MySqlCommand cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@temporada", temporada);

        
        using var reader = cmd.ExecuteReader();
        

        Console.WriteLine($"Temporada: {temporada} (ordenador por {ordenarPor}):");

        while (reader.Read())
        {
            Console.WriteLine($"Equipo: {reader["Equipo"]}, {ordenarPor}: {reader[ordenarPor]}");
        }
    }
}
