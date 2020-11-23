using LogIn.DAL;
using LogIn.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace LogIn.BLL
{
    public class UsuarioBLL
    {
        public static bool Guardar(Usuario usuario)
        {
            if (!Existe(usuario.Nombre))
                return Insertar(usuario);
            else
                return Modificar(usuario);
        }
        private static bool Insertar(Usuario usuario)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Usuario.Add(usuario);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Modificar(Usuario usuario)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(usuario).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static bool Eliminar(string nombre)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var usuario = contexto.Usuario.Find(nombre);
                if (usuario != null)
                {
                    contexto.Usuario.Remove(usuario);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static Usuario Buscar(string nombre)
        {
            Contexto contexto = new Contexto();
            Usuario usuario;
            try
            {
                usuario = contexto.Usuario.Find(nombre);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return usuario;
        }

        public static bool Existe(string nombre)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;
            try
            {
                encontrado = contexto.Usuario.Any(u => u.Nombre == nombre);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return encontrado;
        }

        public static List<Usuario> GetList(Expression<Func<Usuario, bool>> criterio)
        {
            List<Usuario> lista = new List<Usuario>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Usuario.Where(criterio).AsNoTracking().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }

        public static List<Usuario> GetList()
        {
            List<Usuario> lista = new List<Usuario>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Usuario.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
        public static bool Validar(string nombre, string clave)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                paso = contexto.Usuario.Any(u => u.Nombre == nombre && u.Clave == clave );
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
        public static string getHashSha256(string clave)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(clave);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}

