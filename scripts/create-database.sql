CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250108020942_Produto') THEN
    CREATE TABLE "Produtos" (
        "Id" uuid NOT NULL,
        "Nome" varchar(100),
        "Descricao" varchar(200) NOT NULL,
        "Imagem" varchar(255),
        "Preco" numeric NOT NULL,
        CONSTRAINT "PK_Produtos" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250108020942_Produto') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250108020942_Produto', '9.0.0');
    END IF;
END $EF$;
COMMIT;

