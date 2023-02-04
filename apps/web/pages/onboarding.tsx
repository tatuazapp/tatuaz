import { Center } from "@chakra-ui/react"
import type { NextPage } from "next"
import { FormattedMessage } from "react-intl"
import AppLayout from "../components/auth/AppLayout"
import { PageContentWrapper } from "../components/common/PageContentWrapper/styles"
import { PreferencesTitles } from "../components/onboarding/preferences/PreferencesTitle/styles"

const Index: NextPage = () => (
  <AppLayout>
    <PageContentWrapper>
      <Center>
        <PreferencesTitles>
          <FormattedMessage
            defaultMessage="Jakie dziarki lubisz byku"
            id="55O84p"
          />
        </PreferencesTitles>
      </Center>
    </PageContentWrapper>
  </AppLayout>
)

export default Index
