/* This content is licensed under the terms of the Creative Commons Attribution 4.0 International License.
 * When using this content, you must:
 * •    Acknowledge that the content is from the Sansar Knowledge Base.
 * •    Include our copyright notice: "© 2017 Linden Research, Inc."
 * •    Indicate that the content is licensed under the Creative Commons Attribution-Share Alike 4.0 International License.
 * •    Include the URL for, or link to, the license summary at https://creativecommons.org/licenses/by-sa/4.0/deed.hi (and, if possible, to the complete license terms at https://creativecommons.org/licenses/by-sa/4.0/legalcode.
 * For example:
 * "This work uses content from the Sansar Knowledge Base. © 2017 Linden Research, Inc. Licensed under the Creative Commons Attribution 4.0 International License (license summary available at https://creativecommons.org/licenses/by/4.0/ and complete license terms available at https://creativecommons.org/licenses/by/4.0/legalcode)."
 */

using Sansar.Script;
using Sansar.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptLibrary
{
    [Tooltip("Do This template in response to simple script events.")]
    [DisplayName(nameof(DoThisSimpleScriptTemplate))]
    public class DoThisSimpleScriptTemplate : LibraryBase // Broadcaster settings require ScenePrivate
    {
        #region EditorProperties
        /*
		 * 
		 * 
        [Tooltip("Additional Event for Doing Somthing This with the agent. Can be a comma separated list of event names.")]
        [DefaultValue("")]
        [DisplayName("-> Start SomethingElse This with Agent")]
        public readonly string StartDoingElseEvent;


        */
        [Tooltip("Start Doing This with the agent. Can be a comma separated list of event names.")]
        [DefaultValue("on")]
        [DisplayName("-> Start Doing This with Agent")]
        public readonly string StartDoingThisEvent;

        [Tooltip("Stop broadcasting the agent. Can be a comma separated list of event names.")]
        [DefaultValue("off")]
        [DisplayName("-> Stop Stop Doing This with Agent")]
        public readonly string StopDoingThisEvent;

        [Tooltip("Enable responding to events for this script. Can be a comma separated list of event names.")]
        [DefaultValue("doingthis_enable")]
        [DisplayName("-> Enable")]
        public readonly string EnableEvent;

        [Tooltip("Disable responding to events for this script. Can be a comma separated list of event names.")]
        [DefaultValue("doingthis_disable")]
        [DisplayName("-> Disable")]
        public readonly string DisableEvent;

        [Tooltip(@"If StartEnabled is true then the script will respond to events when the scene is loaded
        If StartEnabled is false then the script will not respond to events until an (-> Enable) event is received.")]
        [DefaultValue(true)]
        [DisplayName("Start Enabled")]
        public readonly bool StartEnabled = true;

        #endregion

        // Needed to Accumulate subscriptions to store for Unsubscriptions
        Action subscription = null;

        // Your  Do Thise data you need to use with Agents
        

        protected override void SimpleInit()
        {
            // Setup Items for Doing this 


            
            // This code to consistently handle Enable/Disable Event
            if (StartEnabled) Subscribe(null);

            SubscribeToAll(EnableEvent, Subscribe);
            SubscribeToAll(DisableEvent, Unsubscribe);
        }

        private void Subscribe(ScriptEventData sed)
        {
            if (subscription == null)
            {
                subscription = SubscribeToAll(StartDoingThisEvent, (ScriptEventData subdata) =>
                {
                    ISimpleData simpledata = subdata.Data?.AsInterface<ISimpleData>();
                    if (simpledata != null && simpledata.AgentInfo != null)
                    {
                        AgentPrivate agent = ScenePrivate.FindAgent(simpledata.AgentInfo.SessionId);

                        if (agent != null && agent.IsValid)
                        {
                            // Start Do something with he Agent that was in the Simpledata 
                            // recieved in the Event Subscribed to.
                        }
                    }
                });

                subscription += SubscribeToAll(StopDoingThisEvent, (ScriptEventData subdata) =>
                {
                    ISimpleData simpledata = subdata.Data?.AsInterface<ISimpleData>();
                    if (simpledata != null && simpledata.AgentInfo != null)
                    {
                        AgentPrivate agent = ScenePrivate.FindAgent(simpledata.AgentInfo.SessionId);

                        if (agent != null && agent.IsValid)
                        {
                            // Stop Do something with he Agent that was in the Simpledata 
                            // recieved in the Event Subscribed to.
                            
                        }
                    }
                });

                /* Template for additonal events to be responded to ...
               
                subscription += SubscribeToAll(StartSomethingElseEvent, (ScriptEventData subdata) =>
                {
                    ISimpleData simpledata = subdata.Data?.AsInterface<ISimpleData>();
                    if (simpledata != null && simpledata.AgentInfo != null)
                    {
                        AgentPrivate agent = ScenePrivate.FindAgent(simpledata.AgentInfo.SessionId);

                        if (agent != null && agent.IsValid)
                        {
                            // Stop Do somethingElse with he Agent that was in the Simpledata 
                            // recieved in the Event Subscribed to.

                        }
                    }
                });
                 .......................................................................*/


            }
        }

        private void Unsubscribe(ScriptEventData sed)
        {
            // code needed to handle the nsubscription

            if (subscription != null)
            {
                subscription();
                subscription = null;
            }
        }
    }
}
