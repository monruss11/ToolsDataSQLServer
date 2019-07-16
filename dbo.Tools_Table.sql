CREATE TABLE [dbo].[Tools_Table] (
    [ID]            INT        NOT NULL,
    [Type]          TEXT       NULL,
    [Diametr]       INT        NULL,
    [Length]        INT        NULL,
    [Cut_Length]    INT        NULL,
    [Corner_Radius] FLOAT (53) NULL,
    [Amount]        INT        NULL,
    [Date_Added]    TEXT       NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

