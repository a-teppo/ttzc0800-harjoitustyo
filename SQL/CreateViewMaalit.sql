CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `M3203`@`%` 
    SQL SECURITY DEFINER
VIEW `Maalit` AS
    SELECT 
        `Henkilot`.`HenkiloID` AS `HenkiloID`,
        COUNT(`Maali`.`Maalintekija`) AS `Maalit`
    FROM
        ((`Henkilot`
        JOIN `Maali` ON ((`Henkilot`.`HenkiloID` = `Maali`.`Maalintekija`)))
    GROUP BY `Henkilot`.`HenkiloID`