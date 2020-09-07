CREATE PROC sp_SQLToHTMLTable
(
 @table VARCHAR(100),
 @database varchar(100) = null,
 @Combine NVARCHAR(MAX) OUT
  )
AS
BEGIN
    DECLARE @TempTable AS VARCHAR(100); --We created this in order to get rid of Schema in original table variable
    SET @TempTable = SUBSTRING(@table, CHARINDEX('.', @table) + 1, LEN(@table));
	DECLARE @XMLHeading AS XML;
	DECLARE @XMLHeadingStr1 AS nvarchar(max);
	
	if @database is null
	begin
		set @database = 'tempdb'
	end

    SET @XMLHeadingStr1 = N' use ' + @database + '
	set @XMLHeading = 
    (
        SELECT
            (
				select name 
				from sys.columns (nolock)
				where object_id = object_id(''' + @table + ''') order by column_id
                FOR XML PATH(''''), TYPE
            )

        FOR XML PATH(''tr'')
    )
	SET @XMLHeading = @XMLHeading.query(''for $tr in /tr
        return <tr>
        {
            for $COLUMN_NAME in $tr/*
            return <th>{$COLUMN_NAME/text()}</th>
        }
        </tr>'');	
	';

    EXECUTE sp_executesql @XMLHeadingStr1, N'@XMLHeading XML OUTPUT', @XMLHeading OUTPUT;

    --Create table body
    DECLARE @sql AS NVARCHAR(MAX);
    DECLARE @XMLBody AS XML;
    --this query take all the values in the table and change them in HTML table form
    SET @sql
        = N'SET @XMLBODY = 
			(SELECT  ( SELECT top 500 * FROM ' + @table
          + N' FOR XML PATH(''tr''), TYPE, ELEMENTS XSINIL)
			.query(''for $tr in /tr return <tr> 
			{ for $td in $tr/* return <td>{$td/text()}</td> } </tr>'') 
			)';
    EXECUTE sys.sp_executesql @sql, N'@XMLBODY XML OUTPUT', @XMLBody OUTPUT;



    DECLARE @Style AS NVARCHAR(MAX);
    --This query define the style of the HTML table, I choose basic one, it is open for improvement
    SET @Style
        = N'	<style>table {    font-family: "Trebuchet MS", Arial, Helvetica, sans-serif; border-collapse:collapse; width: 85%; font-size: 12px; } 
				table.center {margin-left:auto; margin-right:auto; } 
				td, th {border: 1px solid #aaaaaa;padding: 8px;}table tr:nth-child(even){background-color: #f2f2f2;} 
				table tr:nth-child(even) {background: #ddd} tr:nth-child(odd) {background: #FFF}  tr:hover {background-color: #eee;} 
				table th { padding-top: 12px;padding-bottom: 12px;text-align: left; background-color: 	#0000A0;color: white;}</style>
		   ';


    --Combine all XML and Style variables
    DECLARE @XMLHeadingSTR AS NVARCHAR(MAX),@XMLBodySTR AS NVARCHAR(MAX)
	SET @XMLBodySTR = CONVERT(NVARCHAR(MAX), @XMLBody);
	SET @XMLHeadingSTR = CONVERT(NVARCHAR(MAX), @XMLHeading);


    SET @Combine
        = CAST(@Style AS NVARCHAR(MAX)) + CAST('<div align=center><table>' AS NVARCHAR(50)) + @XMLHeadingSTR + @XMLBodySTR  + CAST('</table></div>' AS NVARCHAR(50));
    SELECT @Combine


END;
