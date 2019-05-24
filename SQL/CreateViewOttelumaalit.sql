CREATE VIEW `Ottelumaalit` AS
    SELECT 
        Ottelu,
        COUNT(0) AS Maalit, Joukkue
    FROM
        Maali
    GROUP BY Ottelu , Joukkue