CREATE PROCEDURE GetTotalPrice
AS
BEGIN
    SELECT SUM(price) AS TotalPrice FROM OrderItems;
END
