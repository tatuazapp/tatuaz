import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalCloseButton,
  ModalBody,
  Button,
  Center,
  useToast,
} from "@chakra-ui/react"
import { useMutation } from "@tanstack/react-query"
import { Paragraph } from "@tatuaz/ui"
import { FunctionComponent } from "react"
import { FormattedMessage, useIntl } from "react-intl"
import { api } from "../../../../api/apiClient"
import { queryKeys } from "../../../../api/queryKeys"
import { queryClient } from "../../../../pages/_app"
import { UserProfile } from "../../../../pages/dashboard/profile"

type UserSettingsModalProps = {
  isModalOpen: boolean
  setIsModalOpen: (value: boolean) => void
  user: UserProfile | undefined | null
}

const UserSettingsModal: FunctionComponent<UserSettingsModalProps> = ({
  user,
  isModalOpen,
  setIsModalOpen,
}) => {
  const intl = useIntl()

  const toast = useToast()

  const changeAccountTypeMutation = useMutation({
    mutationFn: (artist: boolean) =>
      api.identity.setAccountType({
        artist,
      }),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [queryKeys.whoAmI],
      })
    },
    onError: () => {
      queryClient.invalidateQueries({
        queryKey: [queryKeys.whoAmI],
      })
    },
  })

  const handleCloseModal = () => {
    setIsModalOpen(false)
  }

  const changeAccountType = (artist: boolean) => {
    changeAccountTypeMutation.mutate(artist)
    toast({
      title: intl.formatMessage({
        defaultMessage: "Zmieniono typ konta",
        id: "saad1w",
      }),
      status: "success",
      duration: 5000,
      isClosable: true,
    })

    handleCloseModal()
  }

  return (
    <Modal isOpen={isModalOpen} onClose={handleCloseModal}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>
          <FormattedMessage defaultMessage="Ustawienia" id="rvneDJ" />
        </ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          {user?.artist ? (
            <>
              <Paragraph level={2}>
                <FormattedMessage
                  defaultMessage="Czy chcesz zmienić typ konta na standardowe?"
                  id="RLYwbq"
                />
              </Paragraph>
              <Center>
                <Button
                  colorScheme="primary"
                  mb={10}
                  mt={10}
                  onClick={() => changeAccountType(false)}
                >
                  <FormattedMessage
                    defaultMessage="Przywróć konto standardowe"
                    id="s/5XIF"
                  />
                </Button>
              </Center>
            </>
          ) : (
            <>
              <Paragraph level={2}>
                <FormattedMessage
                  defaultMessage="Chcesz zarabiać na swojej pasji? Zostań artystą i przyjmuj klientów z całego świata!"
                  id="2F8IoI"
                />
              </Paragraph>
              <Center>
                <Button
                  colorScheme="primary"
                  mb={10}
                  mt={10}
                  onClick={() => changeAccountType(true)}
                >
                  <FormattedMessage
                    defaultMessage="Zostań artystą"
                    id="VWvyaF"
                  />
                </Button>
              </Center>
            </>
          )}
        </ModalBody>
      </ModalContent>
    </Modal>
  )
}

export default UserSettingsModal
