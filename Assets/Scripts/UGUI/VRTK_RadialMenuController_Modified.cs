namespace VRTK
{
    using UnityEngine;

    [RequireComponent(typeof(VRTK_RadialMenu))]
    public class VRTK_RadialMenuController_Modified : MonoBehaviour
    {
        [Tooltip("The controller to listen to the controller events on.")]
        public VRTK_ControllerEvents events;
        public VRTK_Pointer pointer;

        protected VRTK_RadialMenu menu;
        protected TouchAngleDeflection currentTad; //Keep track of angle and deflection for when we click
        protected bool touchpadTouched;
        protected bool IsMenuShown;

        protected virtual void Awake()
        {
            menu = GetComponent<VRTK_RadialMenu>();
            pointer = GetComponent<VRTK_Pointer>();
            Initialize();
        }

        protected virtual void Initialize()
        {
            if (events == null)
            {
                events = GetComponentInParent<VRTK_ControllerEvents>();
            }
            if (pointer == null)
            {
                pointer = GetComponentInParent<VRTK_Pointer>();
            }
        }

        protected virtual void OnEnable()
        {
            if (events == null)
            {
                VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_NOT_INJECTED, "RadialMenuController", "VRTK_ControllerEvents", "events", "the parent"));
                return;
            }
            else
            {
                events.ButtonTwoPressed += new ControllerInteractionEventHandler(MenuButtonClick);
                events.TouchpadPressed += new ControllerInteractionEventHandler(DoTouchpadClicked);
                events.TouchpadReleased += new ControllerInteractionEventHandler(DoTouchpadUnclicked);
                events.TouchpadTouchStart += new ControllerInteractionEventHandler(DoTouchpadTouched);
                events.TouchpadTouchEnd += new ControllerInteractionEventHandler(DoTouchpadUntouched);
                events.TouchpadAxisChanged += new ControllerInteractionEventHandler(DoTouchpadAxisChanged);

                menu.FireHapticPulse += new HapticPulseEventHandler(AttemptHapticPulse);
            }
        }

        protected virtual void OnDisable()
        {
            events.ButtonTwoPressed -= new ControllerInteractionEventHandler(MenuButtonClick);
            events.TouchpadPressed -= new ControllerInteractionEventHandler(DoTouchpadClicked);
            events.TouchpadReleased -= new ControllerInteractionEventHandler(DoTouchpadUnclicked);
            events.TouchpadTouchStart -= new ControllerInteractionEventHandler(DoTouchpadTouched);
            events.TouchpadTouchEnd -= new ControllerInteractionEventHandler(DoTouchpadUntouched);
            events.TouchpadAxisChanged -= new ControllerInteractionEventHandler(DoTouchpadAxisChanged);

            menu.FireHapticPulse -= new HapticPulseEventHandler(AttemptHapticPulse);
        }

        protected virtual void MenuButtonClick(object sender, ControllerInteractionEventArgs e)
        {
            if (!IsMenuShown)
            {
                DoShowMenu(CalculateAngle(e));
                pointer.enabled = false;
            }
            else
            {
                DoHideMenu(false);
                pointer.enabled = true;
            }
            IsMenuShown = !IsMenuShown;
        }

        protected virtual void DoClickButton(object sender = null) // The optional argument reduces the need for middleman functions in subclasses whose events likely pass object sender
        {
            if (IsMenuShown)
            {
                menu.ClickButton(currentTad);
                DoHideMenu(false);
                pointer.enabled = true;
                IsMenuShown = !IsMenuShown;
            }       
        }

        protected virtual void DoUnClickButton(object sender = null)
        {
            if (IsMenuShown)
            {
                menu.UnClickButton(currentTad);
            }              
        }

        protected virtual void DoShowMenu(TouchAngleDeflection initialTad, object sender = null)
        {
            menu.ShowMenu();
            DoChangeAngle(initialTad); // Needed to register initial touch position before the touchpad axis actually changes
        }

        protected virtual void DoHideMenu(bool force, object sender = null)
        {
            menu.StopTouching();
            menu.HideMenu(force);
        }

        protected virtual void DoChangeAngle(TouchAngleDeflection givenTouchAngleDeflection, object sender = null)
        {
            currentTad = givenTouchAngleDeflection;

            menu.HoverButton(currentTad);
        }

        protected virtual void AttemptHapticPulse(float strength)
        {
            if (events)
            {
                VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(events.gameObject), strength);
            }
        }

        protected virtual void DoTouchpadClicked(object sender, ControllerInteractionEventArgs e)
        {
            DoClickButton();
        }

        protected virtual void DoTouchpadUnclicked(object sender, ControllerInteractionEventArgs e)
        {
            DoUnClickButton();
        }

        protected virtual void DoTouchpadTouched(object sender, ControllerInteractionEventArgs e)
        {
            if (IsMenuShown)
                touchpadTouched = true;           
        }

        protected virtual void DoTouchpadUntouched(object sender, ControllerInteractionEventArgs e)
        {
            if (IsMenuShown)
                touchpadTouched = false;            
        }

        //Menu button pressed
        protected virtual void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            if (touchpadTouched && IsMenuShown)
            {
                DoChangeAngle(CalculateAngle(e));
            }
        }

        protected virtual TouchAngleDeflection CalculateAngle(ControllerInteractionEventArgs e)
        {
            TouchAngleDeflection touchAngleDeflection = new TouchAngleDeflection();
            touchAngleDeflection.angle = 360 - e.touchpadAngle;
            touchAngleDeflection.deflection = e.touchpadAxis.magnitude;
            return touchAngleDeflection;
        }
    }
}
