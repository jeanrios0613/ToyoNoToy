using System;
using System.Collections.Generic;

namespace ElChenVuelveDashb.Models;

public partial class ProcessDefinitionSequential
{
    public Guid BusinessProcessDefinitionId { get; set; }

    public string? Prefix { get; set; }

    public int InitialSequential { get; set; }

    public int ActualSequential { get; set; }

    public int IncrementSequentialBy { get; set; }

    public int AmountOfCharacterOnTheLeft { get; set; }

    public string? CharacterOnTheLeft { get; set; }

    public virtual ProcessDefinition BusinessProcessDefinition { get; set; } = null!;
}
