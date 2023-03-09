import { Center } from "@chakra-ui/react"
import { FunctionComponent } from "react"
import { FormattedMessage } from "react-intl"
import { OnboardingTitle } from "../../OnboardingTitle/styles"
import PreferencesPicker from "../PreferencesPicker"

type OnboardingPreferencesProps = {
  selectedPreferences: number[]
  setSelectedPreferences: (preferences: number[]) => void
}

const OnboardingPreferences: FunctionComponent<OnboardingPreferencesProps> = ({
  selectedPreferences,
  setSelectedPreferences,
}) => (
  <>
    <Center>
      <OnboardingTitle>
        <FormattedMessage
          defaultMessage="Wybierz 3 lub więcej kategorii, które Cię interesują"
          id="CDfBXN"
        />
      </OnboardingTitle>
    </Center>
    <PreferencesPicker
      selectedPreferences={selectedPreferences}
      setSelectedPreferences={setSelectedPreferences}
    />
  </>
)

export default OnboardingPreferences
