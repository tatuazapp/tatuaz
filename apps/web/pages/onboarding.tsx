import { Fade } from "@chakra-ui/react"
import type { NextPage } from "next"
import { useEffect, useState } from "react"
import { useForm } from "react-hook-form"
import { api } from "../api/apiClient"
import { queryKeys } from "../api/queryKeys"
import { OnboardingFooterWrapper } from "../components/onboarding/OnboardingFooter/styles"
import { OnboardingPageWrapper } from "../components/onboarding/OnboardingPageWrapper/styles"
import OnboardingPreferences from "../components/onboarding/preferences/OnboardingPreferences"
import OnboardingPreferencesFooterButtons from "../components/onboarding/preferences/OnboardingPreferencesFooterButtons"
import { useOnboardingRedirect } from "../components/onboarding/useOnboardingRedirect"
import OnboardingUsername, {
  OnboardingUsernameFormValues,
} from "../components/onboarding/username/OnboardingUsername"
import OnboardingUsernameFooterButtons from "../components/onboarding/username/OnboardingUsernameFooterButtons"
import useIsMobile from "../utils/hooks/useIsMobile"
import { queryClient } from "./_app"

const Onboarding: NextPage = () => {
  useOnboardingRedirect()

  const [step, setStep] = useState(0)
  const [prevAnimationStatus, setPrevAnimationStatus] = useState<
    "ready" | "animating" | "complete"
  >("complete")

  const usernameForm = useForm<OnboardingUsernameFormValues>({
    mode: "onChange",
  })
  const [selectedPreferences, setSelectedPreferences] = useState<number[]>([])

  const handleNext = () => {
    setPrevAnimationStatus("animating")
    setStep(step + 1)
  }

  const handleBack = () => {
    setPrevAnimationStatus("animating")
    setStep(step - 1)
  }

  useEffect(() => {
    if (step === 0) {
      queryClient.prefetchQuery([queryKeys.photo.listPhotoCategories], () =>
        api.photo.listPhotoCategories({
          pageNumber: 1,
          pageSize: 100, // pagination is not necessary here
        })
      )
    }
  }, [step])

  const isMobile = useIsMobile()

  return (
    <OnboardingPageWrapper>
      <Fade
        key={0}
        unmountOnExit
        in={step === 0 && prevAnimationStatus === "complete"}
        onAnimationComplete={() => setPrevAnimationStatus("complete")}
      >
        <OnboardingUsername form={usernameForm} onNext={handleNext} />
      </Fade>
      <Fade
        key={1}
        unmountOnExit
        in={step === 1 && prevAnimationStatus === "complete"}
        onAnimationComplete={() => setPrevAnimationStatus("complete")}
      >
        <OnboardingPreferences
          selectedPreferences={selectedPreferences}
          setSelectedPreferences={setSelectedPreferences}
        />
      </Fade>

      <OnboardingFooterWrapper
        justifyContent={
          step === 0 ? (isMobile ? "stretch" : "flex-end") : "space-between"
        }
      >
        {step === 0 ? (
          <OnboardingUsernameFooterButtons
            form={usernameForm}
            onNext={handleNext}
          />
        ) : (
          <OnboardingPreferencesFooterButtons
            selectedPreferences={selectedPreferences}
            usernameForm={usernameForm}
            onBack={handleBack}
          />
        )}
      </OnboardingFooterWrapper>
    </OnboardingPageWrapper>
  )
}

export default Onboarding
