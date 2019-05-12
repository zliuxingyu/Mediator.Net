﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mediator.Net.Binding;
using Mediator.Net.TestUtil.Handlers.CommandHandlers;
using Mediator.Net.TestUtil.Messages;
using Mediator.Net.TestUtil.TestUtils;
using Shouldly;
using Xunit;

namespace Mediator.Net.Test.TestContext
{
    public class TestUseCustomReceiveContext : TestBase
    {

        private IMediator _mediator;

        public TestUseCustomReceiveContext()
        {
            ClearBinding();
            var builder = new MediatorBuilder();
            _mediator = builder.RegisterHandlers(() =>
                {
                    var binding = new List<MessageBinding> { new MessageBinding(typeof(TestBaseCommand), typeof(AsyncTestBaseCommandHandler)) };
                    return binding;
                })
                .Build();

        }


        [Fact]
        public async Task Run()
        {
            var context = new CustomReceiveContext<TestBaseCommand>(new TestBaseCommand(Guid.NewGuid()));
            await _mediator.SendAsync<TestBaseCommand>(context);
            RubishBox.Rublish.Contains(nameof(AsyncTestBaseCommandHandler)).ShouldBe(true);
        }
    }

}
