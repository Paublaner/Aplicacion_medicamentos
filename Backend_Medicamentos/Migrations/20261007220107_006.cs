using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend_Medicamentos.Migrations
{
    /// <inheritdoc />
    public partial class _006 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Domingo",
                columns: new[] { "Id", "Cantidad", "Descripcion", "MomentoDia", "Nombre" },
                values: new object[,]
                {
                    { 1, "Una pastilla", "Diurético para eliminar líquidos", "Desayuno", "Furosemida" },
                    { 2, "Una pastilla", "Suplemento de potasio", "Desayuno", "Potasion" },
                    { 3, "Una pastilla", "Control de la glucosa", "Desayuno", "Metformina" },
                    { 4, "Una pastilla", "Control de la glucosa", "Desayuno", "Jardiance" },
                    { 5, "Una pastilla", "Protección del estómago", "Desayuno", "Omeprazol" },
                    { 6, "Media pastilla", "Diurético para eliminar líquidos", "Comida", "Furosemida" },
                    { 7, "Una pastilla", "Prevención de coágulos", "Comida", "Clopidogrel" },
                    { 8, "Diez miligramos", "Tratamiento de problemas de piel", "Comida", "Acitrecina" },
                    { 9, "Una pastilla", "Control de la glucosa", "Cena", "Metformina" },
                    { 10, "Una pastilla", "Control de la presión arterial", "Cena", "Amlodipino" },
                    { 11, "Una pastilla", "Control del colesterol", "Cena", "Atorvastatina" },
                    { 12, "Una pastilla", "Control de las crisis epilépticas", "Cena", "Zebinix" },
                    { 13, "Media pastilla", "Tratamiento del dolor nervioso", "Cena", "Pregabalina" },
                    { 14, "Media pastilla", "Tratamiento de la depresión", "Cena", "Mirtazapina" }
                });

            migrationBuilder.InsertData(
                table: "Jueves",
                columns: new[] { "Id", "Cantidad", "Descripcion", "MomentoDia", "Nombre" },
                values: new object[,]
                {
                    { 1, "Una pastilla", "Diurético para eliminar líquidos", "Desayuno", "Furosemida" },
                    { 2, "Una pastilla", "Suplemento de potasio", "Desayuno", "Potasion" },
                    { 3, "Una pastilla", "Control de la glucosa", "Desayuno", "Metformina" },
                    { 4, "Una pastilla", "Control de la glucosa", "Desayuno", "Jardiance" },
                    { 5, "Una pastilla", "Protección del estómago", "Desayuno", "Omeprazol" },
                    { 6, "Media pastilla", "Diurético para eliminar líquidos", "Comida", "Furosemida" },
                    { 7, "Una pastilla", "Prevención de coágulos", "Comida", "Clopidogrel" },
                    { 8, "Diez miligramos", "Tratamiento de problemas de piel", "Comida", "Acitrecina" },
                    { 9, "Una pastilla", "Control de la glucosa", "Cena", "Metformina" },
                    { 10, "Una pastilla", "Control de la presión arterial", "Cena", "Amlodipino" },
                    { 11, "Una pastilla", "Control del colesterol", "Cena", "Atorvastatina" },
                    { 12, "Una pastilla", "Control de las crisis epilépticas", "Cena", "Zebinix" },
                    { 13, "Media pastilla", "Tratamiento del dolor nervioso", "Cena", "Pregabalina" },
                    { 14, "Media pastilla", "Tratamiento de la depresión", "Cena", "Mirtazapina" }
                });

            migrationBuilder.InsertData(
                table: "Lunes",
                columns: new[] { "Id", "Cantidad", "Descripcion", "MomentoDia", "Nombre" },
                values: new object[,]
                {
                    { 1, "Una pastilla", "Diurético para eliminar líquidos", "Desayuno", "Furosemida" },
                    { 2, "Una pastilla", "Suplemento de potasio", "Desayuno", "Potasion" },
                    { 3, "Una pastilla", "Control de la glucosa", "Desayuno", "Metformina" },
                    { 4, "Una pastilla", "Control de la glucosa", "Desayuno", "Jardiance" },
                    { 5, "Una pastilla", "Protección del estómago", "Desayuno", "Omeprazol" },
                    { 6, "Media pastilla", "Diurético para eliminar líquidos", "Comida", "Furosemida" },
                    { 7, "Una pastilla", "Prevención de coágulos", "Comida", "Clopidogrel" },
                    { 8, "Diez miligramos", "Tratamiento de problemas de piel", "Comida", "Acitrecina" },
                    { 9, "Una pastilla", "Control de la glucosa", "Cena", "Metformina" },
                    { 10, "Una pastilla", "Control de la presión arterial", "Cena", "Amlodipino" },
                    { 11, "Una pastilla", "Control del colesterol", "Cena", "Atorvastatina" },
                    { 12, "Una pastilla", "Control de las crisis epilépticas", "Cena", "Zebinix" },
                    { 13, "Media pastilla", "Tratamiento del dolor nervioso", "Cena", "Pregabalina" },
                    { 14, "Media pastilla", "Tratamiento de la depresión", "Cena", "Mirtazapina" }
                });

            migrationBuilder.InsertData(
                table: "Martes",
                columns: new[] { "Id", "Cantidad", "Descripcion", "MomentoDia", "Nombre" },
                values: new object[,]
                {
                    { 1, "Una pastilla", "Diurético para eliminar líquidos", "Desayuno", "Furosemida" },
                    { 2, "Una pastilla", "Suplemento de potasio", "Desayuno", "Potasion" },
                    { 3, "Una pastilla", "Control de la glucosa", "Desayuno", "Metformina" },
                    { 4, "Una pastilla", "Control de la glucosa", "Desayuno", "Jardiance" },
                    { 5, "Una pastilla", "Protección del estómago", "Desayuno", "Omeprazol" },
                    { 6, "Media pastilla", "Diurético para eliminar líquidos", "Comida", "Furosemida" },
                    { 7, "Una pastilla", "Prevención de coágulos", "Comida", "Clopidogrel" },
                    { 8, "Diez miligramos", "Tratamiento de problemas de piel", "Comida", "Acitrecina" },
                    { 9, "Una pastilla", "Control de la glucosa", "Cena", "Metformina" },
                    { 10, "Una pastilla", "Control de la presión arterial", "Cena", "Amlodipino" },
                    { 11, "Una pastilla", "Control del colesterol", "Cena", "Atorvastatina" },
                    { 12, "Una pastilla", "Control de las crisis epilépticas", "Cena", "Zebinix" },
                    { 13, "Media pastilla", "Tratamiento del dolor nervioso", "Cena", "Pregabalina" },
                    { 14, "Media pastilla", "Tratamiento de la depresión", "Cena", "Mirtazapina" }
                });

            migrationBuilder.InsertData(
                table: "Miercoles",
                columns: new[] { "Id", "Cantidad", "Descripcion", "MomentoDia", "Nombre" },
                values: new object[,]
                {
                    { 1, "Una pastilla", "Diurético para eliminar líquidos", "Desayuno", "Furosemida" },
                    { 2, "Una pastilla", "Suplemento de potasio", "Desayuno", "Potasion" },
                    { 3, "Una pastilla", "Control de la glucosa", "Desayuno", "Metformina" },
                    { 4, "Una pastilla", "Control de la glucosa", "Desayuno", "Jardiance" },
                    { 5, "Una pastilla", "Protección del estómago", "Desayuno", "Omeprazol" },
                    { 6, "Media pastilla", "Diurético para eliminar líquidos", "Comida", "Furosemida" },
                    { 7, "Una pastilla", "Prevención de coágulos", "Comida", "Clopidogrel" },
                    { 8, "Diez miligramos", "Tratamiento de problemas de piel", "Comida", "Acitrecina" },
                    { 9, "Una pastilla", "Control de la glucosa", "Cena", "Metformina" },
                    { 10, "Una pastilla", "Control de la presión arterial", "Cena", "Amlodipino" },
                    { 11, "Una pastilla", "Control del colesterol", "Cena", "Atorvastatina" },
                    { 12, "Una pastilla", "Control de las crisis epilépticas", "Cena", "Zebinix" },
                    { 13, "Media pastilla", "Tratamiento del dolor nervioso", "Cena", "Pregabalina" },
                    { 14, "Media pastilla", "Tratamiento de la depresión", "Cena", "Mirtazapina" }
                });

            migrationBuilder.InsertData(
                table: "Sabado",
                columns: new[] { "Id", "Cantidad", "Descripcion", "MomentoDia", "Nombre" },
                values: new object[,]
                {
                    { 1, "Una pastilla", "Diurético para eliminar líquidos", "Desayuno", "Furosemida" },
                    { 2, "Una pastilla", "Suplemento de potasio", "Desayuno", "Potasion" },
                    { 3, "Una pastilla", "Control de la glucosa", "Desayuno", "Metformina" },
                    { 4, "Una pastilla", "Control de la glucosa", "Desayuno", "Jardiance" },
                    { 5, "Una pastilla", "Protección del estómago", "Desayuno", "Omeprazol" },
                    { 6, "Media pastilla", "Diurético para eliminar líquidos", "Comida", "Furosemida" },
                    { 7, "Una pastilla", "Prevención de coágulos", "Comida", "Clopidogrel" },
                    { 8, "Diez miligramos", "Tratamiento de problemas de piel", "Comida", "Acitrecina" },
                    { 9, "Una pastilla", "Control de la glucosa", "Cena", "Metformina" },
                    { 10, "Una pastilla", "Control de la presión arterial", "Cena", "Amlodipino" },
                    { 11, "Una pastilla", "Control del colesterol", "Cena", "Atorvastatina" },
                    { 12, "Una pastilla", "Control de las crisis epilépticas", "Cena", "Zebinix" },
                    { 13, "Media pastilla", "Tratamiento del dolor nervioso", "Cena", "Pregabalina" },
                    { 14, "Media pastilla", "Tratamiento de la depresión", "Cena", "Mirtazapina" }
                });

            migrationBuilder.InsertData(
                table: "Viernes",
                columns: new[] { "Id", "Cantidad", "Descripcion", "MomentoDia", "Nombre" },
                values: new object[,]
                {
                    { 1, "Una pastilla", "Diurético para eliminar líquidos", "Desayuno", "Furosemida" },
                    { 2, "Una pastilla", "Suplemento de potasio", "Desayuno", "Potasion" },
                    { 3, "Una pastilla", "Control de la glucosa", "Desayuno", "Metformina" },
                    { 4, "Una pastilla", "Control de la glucosa", "Desayuno", "Jardiance" },
                    { 5, "Una pastilla", "Protección del estómago", "Desayuno", "Omeprazol" },
                    { 6, "Media pastilla", "Diurético para eliminar líquidos", "Comida", "Furosemida" },
                    { 7, "Una pastilla", "Prevención de coágulos", "Comida", "Clopidogrel" },
                    { 8, "Diez miligramos", "Tratamiento de problemas de piel", "Comida", "Acitrecina" },
                    { 9, "Una pastilla", "Control de la glucosa", "Cena", "Metformina" },
                    { 10, "Una pastilla", "Control de la presión arterial", "Cena", "Amlodipino" },
                    { 11, "Una pastilla", "Control del colesterol", "Cena", "Atorvastatina" },
                    { 12, "Una pastilla", "Control de las crisis epilépticas", "Cena", "Zebinix" },
                    { 13, "Media pastilla", "Tratamiento del dolor nervioso", "Cena", "Pregabalina" },
                    { 14, "Media pastilla", "Tratamiento de la depresión", "Cena", "Mirtazapina" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Domingo",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Domingo",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Domingo",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Domingo",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Domingo",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Domingo",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Domingo",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Domingo",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Domingo",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Domingo",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Domingo",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Domingo",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Domingo",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Domingo",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Jueves",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Jueves",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Jueves",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Jueves",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Jueves",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Jueves",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Jueves",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Jueves",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Jueves",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Jueves",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Jueves",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Jueves",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Jueves",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Jueves",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Lunes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lunes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lunes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lunes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Lunes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Lunes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Lunes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Lunes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Lunes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Lunes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Lunes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Lunes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Lunes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Lunes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Martes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Martes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Martes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Martes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Martes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Martes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Martes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Martes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Martes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Martes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Martes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Martes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Martes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Martes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Miercoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Miercoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Miercoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Miercoles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Miercoles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Miercoles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Miercoles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Miercoles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Miercoles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Miercoles",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Miercoles",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Miercoles",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Miercoles",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Miercoles",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Sabado",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sabado",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sabado",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sabado",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sabado",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Sabado",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Sabado",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Sabado",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Sabado",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Sabado",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Sabado",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Sabado",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Sabado",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Sabado",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Viernes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Viernes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Viernes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Viernes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Viernes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Viernes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Viernes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Viernes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Viernes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Viernes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Viernes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Viernes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Viernes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Viernes",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
