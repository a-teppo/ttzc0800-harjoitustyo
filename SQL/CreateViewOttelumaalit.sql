CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `M3203`@`%` 
    SQL SECURITY DEFINER
VIEW `Ottelumaalit` AS
    SELECT 
        `Maali`.`Ottelu` AS `Ottelu`,
        COUNT(0) AS `Maalit`,
        `Henkilot`.`JoukkueID` AS `Joukkue`
    FROM
        (`Maali`
        JOIN `Henkilot` ON ((`Henkilot`.`HenkiloID` = `Maali`.`Maalintekija`)))
    GROUP BY `Maali`.`Ottelu` , `Henkilot`.`JoukkueID`