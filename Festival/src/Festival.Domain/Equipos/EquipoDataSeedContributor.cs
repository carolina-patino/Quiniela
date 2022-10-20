using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Festival.Equipos
{
    public class EquipoDataSeederContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Equipo, Guid> _equipoRepository;

        public EquipoDataSeederContributor(IRepository<Equipo, Guid> equipoRepository)
        {
            _equipoRepository = equipoRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _equipoRepository.GetCountAsync() > 0)
            {
                return;
            }

            await _equipoRepository.InsertAsync(
                new Equipo
                {
                    Pais = "Qatar",
                    Siglas = "QA",
                    Grupo = "A",
                    Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
                },
                autoSave: true
            );

            await _equipoRepository.InsertAsync(
                new Equipo
                {
                    Pais = "Ecuador",
                    Siglas = "EC",
                    Grupo = "A",
                    Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
                },
                autoSave: true
            );

            await _equipoRepository.InsertAsync(
                new Equipo
                {
                    Pais = "Senegal",
                    Siglas = "SN",
                    Grupo = "A",
                    Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
                },
                autoSave: true
            );

            await _equipoRepository.InsertAsync(
                new Equipo
                {
                    Pais = "Países Bajos",
                    Siglas = "NL",
                    Grupo = "A",
                    Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
                },
                autoSave: true
            );

            await _equipoRepository.InsertAsync(
                new Equipo
                {
                    Pais = "Inglaterra",
                    Siglas = "GB-ENG",
                    Grupo = "B",
                    Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
                },
                autoSave: true
            );

            await _equipoRepository.InsertAsync(
                new Equipo
                {
                    Pais = "Irán",
                    Siglas = "IR",
                    Grupo = "B",
                    Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
                },
                autoSave: true
            );

            await _equipoRepository.InsertAsync(
                new Equipo
                {
                    Pais = "Estados Unidos",
                    Siglas = "US",
                    Grupo = "B",
                    Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
                },
                autoSave: true
            );

            await _equipoRepository.InsertAsync(
                new Equipo
                {
                    Pais = "Gales",
                    Siglas = "",
                    Grupo = "B",
                    Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
                },
                autoSave: true
            );

            await _equipoRepository.InsertAsync(
                new Equipo
                {
                    Pais = "Argentina",
                    Siglas = "AR",
                    Grupo = "C",
                    Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
                },
                autoSave: true
            );

            await _equipoRepository.InsertAsync(
               new Equipo
               {
                   Pais = "Arabia Saudita",
                   Siglas = "SA",
                   Grupo = "C",
                   Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
               },
               autoSave: true
           );

            await _equipoRepository.InsertAsync(
               new Equipo
               {
                   Pais = "México",
                   Siglas = "MX",
                   Grupo = "C",
                   Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
               },
               autoSave: true
           );

            await _equipoRepository.InsertAsync(
               new Equipo
               {
                   Pais = "Polonia",
                   Siglas = "PL",
                   Grupo = "C",
                   Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
               },
               autoSave: true
           );

            await _equipoRepository.InsertAsync(
               new Equipo
               {
                   Pais = "Francia",
                   Siglas = "FR",
                   Grupo = "D",
                   Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
               },
               autoSave: true
           );

            await _equipoRepository.InsertAsync(
               new Equipo
               {
                   Pais = "Australia",
                   Siglas = "AU",
                   Grupo = "C",
                   Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
               },
               autoSave: true
           );

            await _equipoRepository.InsertAsync(
               new Equipo
               {
                   Pais = "Dinamarca",
                   Siglas = "DK",
                   Grupo = "C",
                   Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
               },
               autoSave: true
           );

            await _equipoRepository.InsertAsync(
              new Equipo
              {
                  Pais = "Túnez",
                  Siglas = "TN",
                  Grupo = "C",
                  Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
              },
              autoSave: true
          );

            await _equipoRepository.InsertAsync(
              new Equipo
              {
                  Pais = "España",
                  Siglas = "ES",
                  Grupo = "E",
                  Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
              },
              autoSave: true
          );

            await _equipoRepository.InsertAsync(
              new Equipo
              {
                  Pais = "Costa Rica",
                  Siglas = "CR",
                  Grupo = "E",
                  Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
              },
              autoSave: true
          );

            await _equipoRepository.InsertAsync(
              new Equipo
              {
                  Pais = "Alemania",
                  Siglas = "DE",
                  Grupo = "E",
                  Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
              },
              autoSave: true
          );

            await _equipoRepository.InsertAsync(
              new Equipo
              {
                  Pais = "Japón",
                  Siglas = "JP",
                  Grupo = "E",
                  Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
              },
              autoSave: true
          );

            await _equipoRepository.InsertAsync(
              new Equipo
              {
                  Pais = "Bélgica",
                  Siglas = "BE",
                  Grupo = "F",
                  Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
              },
              autoSave: true
          );

            await _equipoRepository.InsertAsync(
              new Equipo
              {
                  Pais = "Canadá",
                  Siglas = "CA",
                  Grupo = "F",
                  Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
              },
              autoSave: true
          );

            await _equipoRepository.InsertAsync(
              new Equipo
              {
                  Pais = "Marruecos",
                  Siglas = "MA",
                  Grupo = "F",
                  Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
              },
              autoSave: true
          );

            await _equipoRepository.InsertAsync(
              new Equipo
              {
                  Pais = "Croacia",
                  Siglas = "HR",
                  Grupo = "F",
                  Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
              },
              autoSave: true
          );

            await _equipoRepository.InsertAsync(
              new Equipo
              {
                  Pais = "Brasil",
                  Siglas = "BR",
                  Grupo = "G",
                  Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
              },
              autoSave: true
          );

            await _equipoRepository.InsertAsync(
              new Equipo
              {
                  Pais = "Serbia",
                  Siglas = "RS",
                  Grupo = "G",
                  Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
              },
              autoSave: true
          );

            await _equipoRepository.InsertAsync(
              new Equipo
              {
                  Pais = "Suiza",
                  Siglas = "CH",
                  Grupo = "G",
                  Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
              },
              autoSave: true
          );

            await _equipoRepository.InsertAsync(
              new Equipo
              {
                  Pais = "Camerún",
                  Siglas = "CM",
                  Grupo = "G",
                  Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
              },
              autoSave: true
          );

            await _equipoRepository.InsertAsync(
              new Equipo
              {
                  Pais = "Portugal",
                  Siglas = "PT",
                  Grupo = "H",
                  Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
              },
              autoSave: true
          );

            await _equipoRepository.InsertAsync(
              new Equipo
              {
                  Pais = "Ghana",
                  Siglas = "GH",
                  Grupo = "H",
                  Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
              },
              autoSave: true
          );

            await _equipoRepository.InsertAsync(
              new Equipo
              {
                  Pais = "Uruguay",
                  Siglas = "UY",
                  Grupo = "H",
                  Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
              },
              autoSave: true
          );

            await _equipoRepository.InsertAsync(
              new Equipo
              {
                  Pais = "Corea del Sur",
                  Siglas = "KR",
                  Grupo = "H",
                  Tenant = new Guid("A76D0196-548F-3A75-0F97-3A06C4AE1143")
              },
              autoSave: true
          );
        }
    }
}
