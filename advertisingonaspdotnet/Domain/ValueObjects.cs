using System.ComponentModel.DataAnnotations.Schema;

namespace advertisingonaspdotnet.Domain;


    [ComplexType]
    public record Money(
    decimal Amount,
    string Currency
    );

    [ComplexType]
    public record Address(
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country
    );

    [ComplexType]
    public record DateRange(
    DateOnly StartDate,
    DateOnly EndDate
    );

    [ComplexType]
    public record URL(
    string Href
    );

    [ComplexType]
    public record Email(
    string Value
    );

    [ComplexType]
    public record Percentage(
    decimal Value
    );

    [ComplexType]
    public record FrequencyCap(
    FrequencyScope Scope,
    int MaxImpressions,
    FrequencyPeriod Period
    );

