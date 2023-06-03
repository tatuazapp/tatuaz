import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Button,
  useToast,
  RadioGroup,
  Radio,
  Stack,
} from "@chakra-ui/react"
import { useMutation } from "@tanstack/react-query"
import { useState } from "react"
import { FormattedMessage, useIntl } from "react-intl"
import { api } from "../../../../api/apiClient"
import { queryKeys } from "../../../../api/queryKeys"
import { queryClient } from "../../../../pages/_app"

const BookingRequestModal = ({
  isOpen,
  onClose,
  bookingRequestId,
}: {
  isOpen: boolean
  onClose: () => void
  bookingRequestId: number
}) => {
  const [accept, setAccept] = useState(false)
  const toast = useToast()
  const intl = useIntl()

  const respond = useMutation({
    mutationFn: (accept: boolean) =>
      api.booking.respondToBookingRequest({
        bookingRequestId,
        accept,
      }),

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [queryKeys.listIncomingBookingRequests],
      })
    },
    onError: () => {
      queryClient.invalidateQueries({
        queryKey: [queryKeys.listIncomingBookingRequests],
      })

      toast({
        title: intl.formatMessage({
          defaultMessage: "Coś poszło nie tak. Spróbuj ponownie później",
          id: "g1Snh+",
        }),
        status: "error",
        duration: 5000,
      })
    },
  })

  const handleSubmit = () => {
    respond.mutate(accept)
    onClose()
  }

  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>
          <FormattedMessage
            defaultMessage="Odpowiedz na zapytanie o wizytę"
            id="xFTVXQ"
          />
        </ModalHeader>
        <ModalBody>
          <p>
            <FormattedMessage
              defaultMessage="Czy chcesz zaakceptować to zapytanie?"
              id="LZO9gj"
            />
          </p>
          <RadioGroup
            colorScheme="primary"
            defaultValue="reject"
            mt={15}
            onChange={(value) => setAccept(value === "accept")}
          >
            <Stack spacing={4}>
              <Radio value="accept">
                <FormattedMessage defaultMessage="Akceptuj" id="aBD/uW" />
              </Radio>
              <Radio value="reject">
                <FormattedMessage defaultMessage="Odrzuć" id="ZdAM5f" />
              </Radio>
            </Stack>
          </RadioGroup>
        </ModalBody>
        <ModalFooter>
          <Button colorScheme="primary" mr={3} onClick={handleSubmit}>
            <FormattedMessage defaultMessage="Wyślij" id="U/ftc+" />
          </Button>
          <Button isLoading={respond.isLoading} onClick={onClose}>
            <FormattedMessage defaultMessage="Anuluj" id="JmR+Nv" />
          </Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  )
}

export default BookingRequestModal
