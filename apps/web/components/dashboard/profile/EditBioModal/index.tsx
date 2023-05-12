import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalCloseButton,
  ModalBody,
  ModalFooter,
  Button,
  useToast,
  Input,
  FormControl,
  FormLabel,
  Textarea,
  VStack,
} from "@chakra-ui/react"
import { useMutation } from "@tanstack/react-query"
import { FunctionComponent } from "react"
import { useForm } from "react-hook-form"
import { FormattedMessage, useIntl } from "react-intl"
import { api } from "../../../../api/apiClient"
import { queryKeys } from "../../../../api/queryKeys"
import { SetBioDto } from "../../../../api/tatuazApi"
import { queryClient } from "../../../../pages/_app"
import {
  ErrorApiResponse,
  SetBioDtoErrorCode,
} from "../../../../types/apiErrors"
import ApiErrorHandler from "../../../../utils/errors/ApiErrorHandler"

type EditBioModalProps = {
  isOpen: boolean
  onClose: () => void
  initialValues?: {
    bio?: string
    city?: string
  }
}

const EditBioModal: FunctionComponent<EditBioModalProps> = ({
  isOpen,
  onClose,
  initialValues = {},
}) => {
  const intl = useIntl()
  const toast = useToast()
  const {
    register,
    handleSubmit,
    setError,
    formState: { isSubmitting },
  } = useForm<SetBioDto>({
    defaultValues: {
      bio: initialValues.bio ?? "",
      city: initialValues.city ?? "",
    },
  })

  const mutation = useMutation({
    mutationFn: (data: SetBioDto) => api.identity.setBio(data),
    onError: (res: ErrorApiResponse) => {
      const handler = new ApiErrorHandler<SetBioDtoErrorCode>(res.error)

      handler
        .handle("BioTooLong", () => {
          setError("bio", {
            type: "manual",
            message: intl.formatMessage({
              defaultMessage: "Bio jest za długie",
              id: "Q+mZoQ",
            }),
          })
        })
        .handle("CityTooLong", () => {
          setError("city", {
            type: "manual",
            message: intl.formatMessage({
              defaultMessage: "Nazwa miasta jest za długa",
              id: "cPjild",
            }),
          })
        })
        .handle("*", () => {
          toast({
            title: intl.formatMessage({
              defaultMessage:
                "Wystąpił błąd podczas aktualizacji. Spróbuj ponownie później",
              id: "qP/bHU",
            }),
            status: "error",
            position: "top",
          })
        })
        .run()
    },
    onSuccess: () => {
      toast({
        title: intl.formatMessage({
          defaultMessage: "Bio i Miasto zostały zaktualizowane",
          id: "QwvT2h",
        }),
        status: "success",
        position: "top",
      })
      queryClient.invalidateQueries({
        queryKey: [queryKeys.whoAmI],
      })
      onClose()
    },
  })

  const onSubmit = handleSubmit((data) => {
    mutation.mutateAsync(data)
  })

  return (
    <Modal isOpen={isOpen} size="xl" onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>
          <FormattedMessage defaultMessage="Zmień Bio i Miasto" id="TF7OP8" />
        </ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <VStack align="stretch" spacing={4}>
            <FormControl>
              <FormLabel>
                <FormattedMessage defaultMessage="Miasto" id="STzyfo" />
              </FormLabel>
              <Input
                placeholder="Miasto"
                {...register("city")}
                maxLength={64}
              />
            </FormControl>
            <FormControl>
              <FormLabel>
                <FormattedMessage defaultMessage="Bio" id="2W0f9h" />
              </FormLabel>
              <Textarea
                placeholder="Bio"
                {...register("bio")}
                maxLength={4096}
              />
            </FormControl>
          </VStack>
        </ModalBody>

        <ModalFooter>
          <Button mr={3} variant="ghost" onClick={onClose}>
            <FormattedMessage defaultMessage="Anuluj" id="JmR+Nv" />
          </Button>
          <Button
            colorScheme="primary"
            isLoading={isSubmitting}
            onClick={onSubmit}
          >
            <FormattedMessage defaultMessage="Zapisz" id="Jt6oMg" />
          </Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  )
}

export default EditBioModal
