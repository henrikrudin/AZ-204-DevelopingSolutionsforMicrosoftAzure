﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;

public class Program
{
    private const string topicEndpoint = "https://hrtopicsidney.eastus-1.eventgrid.azure.net/api/events";

    private const string topicKey = "zYOUPinkD7kmPEsFQQ4dUMqs2TXEO93goDfze0SbC5c=";

    public static async Task Main(string[] args)
    {
        TopicCredentials credentials = new TopicCredentials(topicKey);
        EventGridClient client = new EventGridClient(credentials);

        List<EventGridEvent> events = new List<EventGridEvent>();

        Person firstPerson = new Person();
        EventGridEvent firstEvent = new EventGridEvent
        {
            Id = Guid.NewGuid().ToString(),
            EventType = "Employees.Registration.New",
            EventTime = DateTime.Now,
            Subject = $"New Employee: {firstPerson.FullName}",
            Data = firstPerson,
            DataVersion = "1.0.0"
        };
        events.Add(firstEvent);

        Person secondPerson = new Person();
        EventGridEvent secondEvent = new EventGridEvent
        {
            Id = Guid.NewGuid().ToString(),
            EventType = "Employees.Registration.New",
            EventTime = DateTime.Now,
            Subject = $"New Employee: {secondPerson.FullName}",
            Data = secondPerson,
            DataVersion = "1.0.0"
        };
        events.Add(secondEvent);

        string topicHostname = new Uri(topicEndpoint).Host;
        await client.PublishEventsAsync(topicHostname, events);

        Console.WriteLine("Events published");
    }
}