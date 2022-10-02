using System;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasySubscriptions
    {

        public void Subscribe(ILoginSession loginSession)
        {
            loginSession.AllowedSubscriptions.AfterKeyAdded += OnAddAllowedSubscription;
            loginSession.AllowedSubscriptions.BeforeKeyRemoved += OnRemoveAllowedSubscription;

            loginSession.BlockedSubscriptions.AfterKeyAdded += OnAddBlockedSubscription;
            loginSession.BlockedSubscriptions.BeforeKeyRemoved += OnRemoveBlockedSubscription;

            loginSession.PresenceSubscriptions.AfterKeyAdded += OnAddPresenceSubscription;
            loginSession.PresenceSubscriptions.BeforeKeyRemoved += OnRemovePresenceSubscription;
            loginSession.PresenceSubscriptions.AfterValueUpdated += OnUpdatedPresenceSubscription;

            loginSession.IncomingSubscriptionRequests.AfterItemAdded += OnIncomingSubscriptionRequests;
        }

        public void Unsubscribe(ILoginSession loginSession)
        {
            loginSession.AllowedSubscriptions.AfterKeyAdded -= OnAddAllowedSubscription;
            loginSession.AllowedSubscriptions.BeforeKeyRemoved -= OnRemoveAllowedSubscription;

            loginSession.BlockedSubscriptions.AfterKeyAdded -= OnAddBlockedSubscription;
            loginSession.BlockedSubscriptions.BeforeKeyRemoved -= OnRemoveBlockedSubscription;

            loginSession.PresenceSubscriptions.AfterKeyAdded -= OnAddPresenceSubscription;
            loginSession.PresenceSubscriptions.BeforeKeyRemoved -= OnRemovePresenceSubscription;
            loginSession.PresenceSubscriptions.AfterValueUpdated -= OnUpdatedPresenceSubscription;

            loginSession.IncomingSubscriptionRequests.AfterItemAdded -= OnIncomingSubscriptionRequests;
        }



        #region Subscriptions / Presence Methods


        public void AddAllowedSubscription(string userSIP, ILoginSession loginSession)
        {
            loginSession.BeginAddAllowedSubscription(new AccountId(userSIP), ar =>
            {
                try
                {
                    loginSession.EndAddAllowedSubscription(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }

        public void AddBlockedSubscription(string userSIP, ILoginSession loginSession)
        {
            loginSession.BeginAddBlockedSubscription(new AccountId(userSIP), ar =>
            {
                try
                {
                    loginSession.EndAddBlockedSubscription(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }

        public void AddAllowPresence(string userSIP, ILoginSession loginSession)
        {
            loginSession.BeginAddPresenceSubscription(new AccountId(userSIP), ar =>
            {
                try
                {
                    loginSession.EndAddPresenceSubscription(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }


        public void RemoveAllowedSubscription(string userSIP, ILoginSession loginSession)
        {
            loginSession.BeginRemoveAllowedSubscription(new AccountId(userSIP), ar =>
            {
                try
                {
                    loginSession.EndRemoveAllowedSubscription(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }

        public void RemoveBlockedSubscription(string userSIP, ILoginSession loginSession)
        {
            loginSession.BeginRemoveBlockedSubscription(new AccountId(userSIP), ar =>
            {
                try
                {
                    loginSession.EndRemoveBlockedSubscription(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }

        public void RemoveAllowedPresence(string userSIP, ILoginSession loginSession)
        {
            loginSession.BeginRemovePresenceSubscription(new AccountId(userSIP), ar =>
            {
                try
                {
                    loginSession.EndRemovePresenceSubscription(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }


        #endregion



        #region Subscription / Presence Callbacks


        private void OnAddAllowedSubscription(object sender, KeyEventArg<AccountId> keyArgs)
        {
            var source = (IReadOnlyHashSet<AccountId>)sender;

            EasyEvents.OnAddAllowedSubscription(keyArgs.Key);
        }

        private void OnRemoveAllowedSubscription(object sender, KeyEventArg<AccountId> keyArgs)
        {
            var source = (IReadOnlyHashSet<AccountId>)sender;

            EasyEvents.OnRemoveAllowedSubscription(keyArgs.Key);
        }

        private void OnAddPresenceSubscription(object sender, KeyEventArg<AccountId> keyArgs)
        {
            var source = (VivoxUnity.IReadOnlyDictionary<AccountId, IPresenceSubscription>)sender;

            EasyEvents.OnAddPresenceSubscription(keyArgs.Key);
        }

        private void OnRemovePresenceSubscription(object sender, KeyEventArg<AccountId> keyArgs)
        {
            var source = (VivoxUnity.IReadOnlyDictionary<AccountId, IPresenceSubscription>)sender;

            EasyEvents.OnRemovePresenceSubscription(keyArgs.Key);
        }

        private void OnUpdatedPresenceSubscription(object sender, ValueEventArg<AccountId, IPresenceSubscription> keyArgs)
        {
            var source = (VivoxUnity.IReadOnlyDictionary<AccountId, IPresenceSubscription>)sender;

            EasyEvents.OnUpdatePresenceSubscription(keyArgs);
        }

        private void OnAddBlockedSubscription(object sender, KeyEventArg<AccountId> keyArgs)
        {
            var source = (IReadOnlyHashSet<AccountId>)sender;

            EasyEvents.OnAddBlockedSubscription(keyArgs.Key);
        }

        private void OnRemoveBlockedSubscription(object sender, KeyEventArg<AccountId> keyArgs)
        {
            var source = (IReadOnlyHashSet<AccountId>)sender;
            
            EasyEvents.OnRemoveBlockedSubscription(keyArgs.Key);
        }

        private void OnIncomingSubscriptionRequests(object sender, QueueItemAddedEventArgs<AccountId> subRequests)
        {
            var source = (IReadOnlyQueue<AccountId>)sender;
            while (source.Count > 0)
            {
                // todo check if it works
                var request = source.Dequeue();
                Debug.Log($"Incoming subscription request from - {request.Name}");
                Debug.Log($"Incoming subscription request from - {request.DisplayName}");
                EasyEvents.OnIncomingSubscription(request);
            }
        }


        #endregion
    }
}