CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `M3203`@`%` 
    SQL SECURITY DEFINER
VIEW `Syotot` AS
    SELECT 
        `Henkilot`.`HenkiloID` AS `HenkiloID`,
        COUNT(`Maali`.`Syottaja`) AS `Syotot`
    FROM
        ((`Henkilot`
        JOIN `Maali` ON ((`Henkilot`.`HenkiloID` = `Maali`.`Syottaja`)))
    GROUP BY `Henkilot`.`HenkiloID`