import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  Button,
  FormControl,
  FormLabel,
  Textarea,
  Box,
  Flex,
  useToast,
} from "@chakra-ui/react"
import { useMutation } from "@tanstack/react-query"
import { SingleDatepicker } from "chakra-dayzed-datepicker"
import { startOfDay } from "date-fns"
import { FunctionComponent } from "react"
import { useForm } from "react-hook-form"
import { FormattedMessage, useIntl } from "react-intl"
import { Timeit } from "react-timeit"
import { api } from "../../../../api/apiClient"
import { queryKeys } from "../../../../api/queryKeys"
import { queryClient } from "../../../../pages/_app"
import {
  ErrorApiResponse,
  SendBookingRequestDtoErrorCode,
} from "../../../../types/apiErrors"
import ApiErrorHandler from "../../../../utils/errors/ApiErrorHandler"
import { TimePickerWrapper } from "./styles"

type BookingModalProps = {
  isOpen: boolean
  onClose: () => void
  username: string
}

type BookingForm = {
  date: Date
  startTime: string
  endTime: string
  comment: string
}

const BookingModal: FunctionComponent<BookingModalProps> = ({
  isOpen,
  onClose,
  username,
}) => {
  const { register, handleSubmit, setValue, watch } = useForm<BookingForm>()

  const date = watch("date")

  const toast = useToast()

  const intl = useIntl()

  const { mutate, isLoading } = useMutation({
    mutationFn: api.booking.sendBookingRequest,
    onSuccess: () => {
      toast({
        title: intl.formatMessage({
          defaultMessage: "Wysłano zapytanie o wizytę",
          id: "Piuz8V",
        }),
        status: "success",
        duration: 5000,
        isClosable: true,
      })
      queryClient.invalidateQueries({
        queryKey: [queryKeys.listMyBookingRequests],
      })
      onClose()
    },
    onError: (res: ErrorApiResponse) => {
      if (!res.response) return

      const handler = new ApiErrorHandler<SendBookingRequestDtoErrorCode>(
        res.response.data
      )

      handler
        .handle("StartIsGreaterThanEnd", () => {
          toast({
            title: intl.formatMessage({
              defaultMessage:
                "Godzina rozpoczęcia jest późniejsza niż godzina zakończenia",
              id: "M8xX/L",
            }),
            status: "error",
          })
        })
        .handle("CommentIsTooLong", () => {
          toast({
            title: intl.formatMessage({
              defaultMessage: "Komentarz jest za długi",
              id: "gBQY08",
            }),
            status: "error",
          })
        })

        .handle("*", () => {
          toast({
            title: "Wystąpił błąd",
            status: "error",
            duration: 5000,
            isClosable: true,
          })
        })
        .run()
    },
  })

  const onSubmit = (data: BookingForm) => {
    mutate({
      start: new Date(
        `${data.date.toDateString()} ${data.startTime}`
      ).toISOString(),
      end: new Date(
        `${data.date.toDateString()} ${data.endTime}`
      ).toISOString(),
      comment: data.comment,
      artistName: username,
    })
  }

  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>
          <FormattedMessage defaultMessage="Zapisz się na wizytę" id="YrT4Ud" />
        </ModalHeader>
        <ModalBody>
          <Box mb={4}>
            <FormLabel>
              <FormattedMessage defaultMessage="Data" id="XHHR08" />
            </FormLabel>
            <SingleDatepicker
              date={date}
              disabledDates={new Set([startOfDay(new Date()).getTime()])}
              {...register("date")}
              onDateChange={(date: Date) => setValue("date", date)}
            />
          </Box>
          <Flex justifyContent="space-between" mb={8}>
            <Box>
              <FormLabel>
                <FormattedMessage
                  defaultMessage="Godzina rozpoczęcia"
                  id="BR7gAs"
                />
              </FormLabel>
              <TimePickerWrapper>
                <Timeit
                  {...register("startTime")}
                  onChange={(value: string) => setValue("startTime", value)}
                />
              </TimePickerWrapper>
            </Box>

            <Box>
              <FormLabel>
                <FormattedMessage
                  defaultMessage="Godzina zakończenia"
                  id="0kYM7+"
                />
              </FormLabel>
              <TimePickerWrapper>
                <Timeit
                  {...register("endTime")}
                  onChange={(value: string) => setValue("endTime", value)}
                />
              </TimePickerWrapper>
            </Box>
          </Flex>
          <FormControl mb={4}>
            <FormLabel>
              <FormattedMessage defaultMessage="Komentarz" id="ZigCO9" />
            </FormLabel>
            <Textarea
              placeholder={intl.formatMessage({
                defaultMessage: "Wpisz komentarz",
                id: "lry0De",
              })}
              {...register("comment")}
            />
          </FormControl>
        </ModalBody>
        <ModalFooter>
          <Button mr={3} variant="ghost" onClick={onClose}>
            <FormattedMessage defaultMessage="Anuluj" id="JmR+Nv" />
          </Button>
          <Button
            colorScheme="primary"
            isLoading={isLoading}
            onClick={handleSubmit(onSubmit)}
          >
            <FormattedMessage defaultMessage="Zapisz" id="Jt6oMg" />
          </Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  )
}

export default BookingModal
