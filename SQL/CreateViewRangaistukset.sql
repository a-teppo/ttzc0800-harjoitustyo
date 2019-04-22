CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `M3203`@`%` 
    SQL SECURITY DEFINER
VIEW `Rangaistukset` AS
    SELECT 
        `Henkilot`.`HenkiloID` AS `HenkiloID`,
        SUM(`Rangaistus`.`Kesto`) AS `Rangaistusminuutit`
    FROM
        (`Henkilot`
        JOIN `Rangaistus` ON ((`Henkilot`.`HenkiloID` = `Rangaistus`.`Henkilo`)))
    GROUP BY `Henkilot`.`HenkiloID`